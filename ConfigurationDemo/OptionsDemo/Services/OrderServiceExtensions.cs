using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsDemo.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OrderServiceOptions>(configuration);


            //services.PostConfigure<OrderServiceOptions>(options =>
            //{
            //    options.MaxOrderCount += 100;
            //});

            #region 增加验证

            //services.AddOptions<OrderServiceOptions>().Configure(options =>
            //{
            //    configuration.Bind(options);
            //}).Validate(validation => validation.MaxOrderCount <= 100,
            //"MaxOrderCount 不能大于 100");


            // 属性验证方式
            //services.AddOptions<OrderServiceOptions>().Configure(options =>
            //{
            //    configuration.Bind(options);
            //}).ValidateDataAnnotations();


            // 使用接口
            services.AddOptions<OrderServiceOptions>().Configure(options =>
            {
                configuration.Bind(options);
            }).Services.AddSingleton<IValidateOptions<OrderServiceOptions>, OrderServiceValidateOptions>();


            #endregion

            services.AddSingleton<IOrderService, OrderService>();

            return services;
        }
    }
}
