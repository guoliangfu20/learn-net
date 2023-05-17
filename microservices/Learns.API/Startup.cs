using Exceptionless;
using Learns.API.Extensions;
using Learns.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Learns.API
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
            services.AddControllers().AddNewtonsoftJson(); //支持构造函数序列化

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMediatRServices();

            services.AddMySqlDomainContext(Configuration.GetValue<string>("MySql"));

            services.AddRepositories();

            // 加入发布订阅
            services.AddEventBus(Configuration);

            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.KnownNetworks.Clear();
            //    options.KnownProxies.Clear();
            //    options.ForwardedHeaders = ForwardedHeaders.All;
            //});


            // 允许跨域设置
            services.AddCors(options =>
            {
                options.AddPolicy("api", builder =>
                {
                    builder.WithOrigins("https://localhost:5001").AllowAnyHeader().AllowCredentials().WithExposedHeaders("abc");

                    builder.SetIsOriginAllowed(orgin => true).AllowCredentials().AllowAnyHeader();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseExceptionless();

            //if (Configuration.GetValue("USE_PathBase", false))
            //{
            //    app.Use((context, next) =>
            //    {
            //        context.Request.PathBase = new PathString("/mobile");
            //        return next();
            //    });
            //}

            if (Configuration.GetValue("USE_Forwarded_Headers", false))
            {
                app.UseForwardedHeaders();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var domainContext = scope.ServiceProvider.GetService<DomainContext>();
                //domainContext.Database.EnsureCreated();  // 若是数据库没有创建，此处会执行创建
            }

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var domainContext = scope.ServiceProvider.GetService<OrderingContext>();
            //    //domainContext.Database.EnsureCreated();
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapMetrics();
                endpoints.MapControllers();
            });
        }
    }
}
