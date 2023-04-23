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
            #region ע�����ͬ�������ڵķ���

            // ����
            //services.AddSingleton<IMySingletonService, MySingletonService>();

            //// ������
            //services.AddScoped<IMyScopedService, MyScopedService>();

            //// ˲ʱ
            //services.AddTransient<IMyTransientService, MyTransientService>();

            #endregion

            #region ��ʽע��

            // ֱ��ע��
            services.AddSingleton<IOrderService>(new OrderService());

            //services.AddSingleton<IOrderService>(new OrderServiceEx());


            // ����ģʽע��
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


            #region ����ע��
            // ����Ѿ����ڣ��򲻻�ע��
            //services.TryAddSingleton<IOrderService, OrderService>();

            // 
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());

            #endregion

            #region �滻���Ƴ�

            //services.Replace(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());

            //services.RemoveAll<IOrderService>();

            #endregion


            #region ע�᷺��ģ��

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
