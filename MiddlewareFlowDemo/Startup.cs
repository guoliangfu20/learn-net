using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace MiddlewareFlowDemo
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //app.Use(async (context, next) =>
            //{
            //    //await context.Response.WriteAsync("hello delege");
            //    await next();
            //    await context.Response.WriteAsync(" hello, next");
            //});


            // map
            //app.Map("/guoliang", myBuilder =>
            //{
            //    myBuilder.Use(async (context, next) =>
            //    {
            //        //await context.Response.WriteAsync("hello delege");
            //        await next();
            //        await context.Response.WriteAsync("hello, map");
            //    });
            //});


            //app.MapWhen(context =>  // when ������
            //{
            //    return context.Request.Query.Keys.Contains("order");
            //}, builder =>
            //{
            //    builder.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("hello, map when.");
            //    });
            //});


            // ʹ���Զ����м��
            app.UseMyMiddleware();





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
