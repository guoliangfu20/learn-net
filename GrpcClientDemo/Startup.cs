using GrpcServerDemo.protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Bulkhead;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static GrpcServerDemo.protos.OrderGrpc;

namespace GrpcClientDemo
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
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); //允许使用不加密的HTTP/2协议
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            {
                options.Address = new Uri("https://localhost:5001");
            })
                .ConfigurePrimaryHttpMessageHandler(provider =>
                {
                    var handler = new SocketsHttpHandler();
                    handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true; //允许无效、或自签名证书
                    return handler;
                })
                .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(20, t => TimeSpan.FromSeconds(t * 20)));  // polly 重试策略

            // 自定义重试策略
            // 当响应码是 201时，进行重试
            var reg = services.AddPolicyRegistry();
            reg.Add("retryForever", Policy.HandleResult<HttpResponseMessage>(message =>
            {
                return message.StatusCode == System.Net.HttpStatusCode.Created;
            })
                //.Fallback()  // 自定义
                .RetryForever());

            // 使用上面的策略
            services.AddHttpClient("orderclient").AddPolicyHandlerFromRegistry("retryForever");


            services.AddHttpClient("orderclient_V2").AddPolicyHandlerFromRegistry((registry, message) =>
            {
                return message.Method == HttpMethod.Get ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("retryForever")
                : Policy.NoOpAsync<HttpResponseMessage>();
            });


            #region 熔断

            services.AddHttpClient("orderclient_V3").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(  // 熔断
                handledEventsAllowedBeforeBreaking: 10,  // 报错多少次后熔断
                durationOfBreak: TimeSpan.FromSeconds(10),  // 熔断的时间
                onBreak: (r, t) => { },  // 熔断时触发的事件
                onReset: () => { },  // 熔断恢复时间
                onHalfOpen: () => { }));  // 恢复之前验证

            // 高级熔断
            services.AddHttpClient("orderclient_V3").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
                failureThreshold: 08,  // 多少比例的错误，开始熔断
              samplingDuration: TimeSpan.FromSeconds(10),  // 多行时间内的错误开始熔断
              minimumThroughput: 100,  // 最小请求数
              durationOfBreak: TimeSpan.FromSeconds(10),
              onBreak: (r, t) => { },
              onReset: () => { },
              onHalfOpen: () => { }));


            // 服务降级
            var breakPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
                  failureThreshold: 0.8,
                  samplingDuration: TimeSpan.FromSeconds(10),
                  minimumThroughput: 100,
                  durationOfBreak: TimeSpan.FromSeconds(20),
                  onBreak: (r, t) => { },
                  onReset: () => { },
                  onHalfOpen: () => { });

            var message = new HttpResponseMessage()
            {
                // 构造一个友好响应
                Content = new StringContent("{}")
            };
            var fallback = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(message);
            var retry = Policy<HttpResponseMessage>.Handle<Exception>().WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(1));  // 重试机制
            var fallbackBreak = Policy.WrapAsync(fallback, retry, breakPolicy);  // 组合策略
            services.AddHttpClient("httpv3").AddPolicyHandler(fallbackBreak);

            #endregion



            #region 限流

            var bulk = Policy.BulkheadAsync<HttpResponseMessage>(
                maxParallelization: 30,  // 最大请求数
                maxQueuingActions: 20,  // 队列数
                onBulkheadRejectedAsync: contxt => Task.CompletedTask
                );

            var message2 = new HttpResponseMessage()
            {
                // 构造一个友好响应
                Content = new StringContent("{}")
            };
            var fallback2 = Policy<HttpResponseMessage>.Handle<BulkheadRejectedException>().FallbackAsync(message);
            var fallbackbulk = Policy.WrapAsync(fallback2, bulk);
            services.AddHttpClient("httpv4").AddPolicyHandler(fallbackbulk);

            #endregion


            //services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            //{
            //    options.Address = new Uri("https://localhost:5001");
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGet("/", async context =>
                {

                    OrderGrpcClient service = context.RequestServices.GetService<OrderGrpcClient>();

                    try
                    {
                        var res = service.CreateOrder(new CreateOrderCommand { BuyerId = "951" });
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    await context.Response.WriteAsync("hello order from grpc server.");
                });

            });
        }
    }
}
