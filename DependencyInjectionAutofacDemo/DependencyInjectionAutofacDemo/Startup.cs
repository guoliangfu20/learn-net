using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using DependencyInjectionAutofacDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionAutofacDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }



        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 于之前的注册相反
            //builder.RegisterType<MyService1>().As<IMyServices>();

            // 命名注册
            // 一个服务注册多次，需要用名字区分
            //builder.RegisterType<MyService2>().Named<IMyServices>("NamedMyService2");


            // 属性注册
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyService2>().As<IMyServices>().PropertiesAutowired();


            // AOP
            //builder.RegisterType<MyInterceptor>();
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyService2>().As<IMyServices>().PropertiesAutowired().InterceptedBy(typeof(MyInterceptor)).EnableInterfaceInterceptors();

            // 子容器
            builder.RegisterType<MyNameService>().InstancePerMatchingLifetimeScope("myscope");



        }


        public ILifetimeScope autofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.autofacContainer = app.ApplicationServices.GetAutofacRoot();

            //var services = this.autofacContainer.ResolveNamed<IMyServices>("NamedMyService2");
            //services.ShowCode();

            // 获取没有命名的服务
            //var services2 = this.autofacContainer.Resolve<IMyServices>();
            //services2.ShowCode();


            #region 子容器

            using (var myscope = autofacContainer.BeginLifetimeScope("myscope"))
            {
                var service0 = myscope.Resolve<MyNameService>();
                using (var scope = myscope.BeginLifetimeScope())
                {
                    var service1 = scope.Resolve<MyNameService>();
                    var service2 = scope.Resolve<MyNameService>();
                    Console.WriteLine($"service1=service2:{service1 == service2}");
                    Console.WriteLine($"service1=service0:{service1 == service0}");
                }
            }

            #endregion



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
