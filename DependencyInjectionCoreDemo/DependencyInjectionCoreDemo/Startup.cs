using DependencyInjectionCoreDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionCoreDemo
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
            #region 注册服务不同生命周期的服务

            // 单例
            //services.AddSingleton<IMySingletonService, MySingletonService>();

            //// 作用域
            //services.AddScoped<IMyScopedService, MyScopedService>();

            //// 瞬时
            //services.AddTransient<IMyTransientService, MyTransientService>();

            #endregion

            #region 花式注册

            // 直接注入
            services.AddSingleton<IOrderService>(new OrderService());

            //services.AddSingleton<IOrderService>(new OrderServiceEx());


            // 工厂模式注册
            //services.AddSingleton<IOrderService>(serviceProvider =>
            //{
            //    return new OrderServiceEx();
            //});

            //services.AddScoped<IOrderService>(serviceProvider =>
            //{
            //    // return new OrderService();
            //    return new OrderServiceEx();
            //});

            #endregion


            #region 尝试注册
            // 如果已经存在，则不会注册
            //services.TryAddSingleton<IOrderService, OrderService>();

            // 
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());

            #endregion

            #region 替换，移除

            //services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());

            //services.RemoveAll<IOrderService>();

            #endregion


            #region 注册泛型模板

            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));

            #endregion





            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
