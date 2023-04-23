using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace StartDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults----");
                    //webBuilder.UseStartup<Startup>();

                    // 可以替代 startup类内容.
                    //webBuilder.ConfigureServices(services =>
                    //{
                    //    Console.WriteLine("Startup.ConfigureServices");
                    //    services.AddRazorPages();
                    //});

                    //webBuilder.Configure(app =>
                    //{
                    //    Console.WriteLine("startup.Configure---");

                    //    app.UseHttpsRedirection();
                    //    app.UseStaticFiles();

                    //    app.UseRouting();

                    //    app.UseAuthorization();

                    //    app.UseEndpoints(endpoints =>
                    //    {
                    //        endpoints.MapRazorPages();
                    //    });
                    //});

                })
            .ConfigureServices(service =>
            {
                Console.WriteLine("ConfigureServices---");
            })
            .ConfigureAppConfiguration(app =>
            {
                Console.WriteLine("ConfigureAppConfiguration");
            })
            .ConfigureHostConfiguration(config =>
            {
                Console.WriteLine("ConfigureHostConfiguration---");
            });
    }
}
