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
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); //����ʹ�ò����ܵ�HTTP/2Э��
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            {
                options.Address = new Uri("https://localhost:5001");
            })
                .ConfigurePrimaryHttpMessageHandler(provider =>
                {
                    var handler = new SocketsHttpHandler();
                    handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true; //������Ч������ǩ��֤��
                    return handler;
                })
                .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(20, t => TimeSpan.FromSeconds(t * 20)));  // polly ���Բ���

            // �Զ������Բ���
            // ����Ӧ���� 201ʱ����������
            var reg = services.AddPolicyRegistry();
            reg.Add("retryForever", Policy.HandleResult<HttpResponseMessage>(message =>
            {
                return message.StatusCode == System.Net.HttpStatusCode.Created;
            })
                //.Fallback()  // �Զ���
                .RetryForever());

            // ʹ������Ĳ���
            services.AddHttpClient("orderclient").AddPolicyHandlerFromRegistry("retryForever");


            services.AddHttpClient("orderclient_V2").AddPolicyHandlerFromRegistry((registry, message) =>
            {
                return message.Method == HttpMethod.Get ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("retryForever")
                : Policy.NoOpAsync<HttpResponseMessage>();
            });


            #region �۶�

            services.AddHttpClient("orderclient_V3").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(  // �۶�
                handledEventsAllowedBeforeBreaking: 10,  // ������ٴκ��۶�
                durationOfBreak: TimeSpan.FromSeconds(10),  // �۶ϵ�ʱ��
                onBreak: (r, t) => { },  // �۶�ʱ�������¼�
                onReset: () => { },  // �۶ϻָ�ʱ��
                onHalfOpen: () => { }));  // �ָ�֮ǰ��֤

            // �߼��۶�
            services.AddHttpClient("orderclient_V3").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
                failureThreshold: 08,  // ���ٱ����Ĵ��󣬿�ʼ�۶�
              samplingDuration: TimeSpan.FromSeconds(10),  // ����ʱ���ڵĴ���ʼ�۶�
              minimumThroughput: 100,  // ��С������
              durationOfBreak: TimeSpan.FromSeconds(10),
              onBreak: (r, t) => { },
              onReset: () => { },
              onHalfOpen: () => { }));


            // ���񽵼�
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
                // ����һ���Ѻ���Ӧ
                Content = new StringContent("{}")
            };
            var fallback = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(message);
            var retry = Policy<HttpResponseMessage>.Handle<Exception>().WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(1));  // ���Ի���
            var fallbackBreak = Policy.WrapAsync(fallback, retry, breakPolicy);  // ��ϲ���
            services.AddHttpClient("httpv3").AddPolicyHandler(fallbackBreak);

            #endregion



            #region ����

            var bulk = Policy.BulkheadAsync<HttpResponseMessage>(
                maxParallelization: 30,  // ���������
                maxQueuingActions: 20,  // ������
                onBulkheadRejectedAsync: contxt => Task.CompletedTask
                );

            var message2 = new HttpResponseMessage()
            {
                // ����һ���Ѻ���Ӧ
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
