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
            // ��֮ǰ��ע���෴
            //builder.RegisterType<MyService1>().As<IMyServices>();

            // ����ע��
            // һ������ע���Σ���Ҫ����������
            //builder.RegisterType<MyService2>().Named<IMyServices>("NamedMyService2");


            // ����ע��
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyService2>().As<IMyServices>().PropertiesAutowired();


            // AOP
            //builder.RegisterType<MyInterceptor>();
            //builder.RegisterType<MyNameService>();
            //builder.RegisterType<MyService2>().As<IMyServices>().PropertiesAutowired().InterceptedBy(typeof(MyInterceptor)).EnableInterfaceInterceptors();

            // ������
            builder.RegisterType<MyNameService>().InstancePerMatchingLifetimeScope("myscope");



        }


        public ILifetimeScope autofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.autofacContainer = app.ApplicationServices.GetAutofacRoot();

            //var services = this.autofacContainer.ResolveNamed<IMyServices>("NamedMyService2");
            //services.ShowCode();

            // ��ȡû�������ķ���
            //var services2 = this.autofacContainer.Resolve<IMyServices>();
            //services2.ShowCode();


            #region ������

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
