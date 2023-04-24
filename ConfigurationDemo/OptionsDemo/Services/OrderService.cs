using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace OptionsDemo.Services
{

    public interface IOrderService
    {
        int ShowMaxOrderCount();
    }

    public class OrderService : IOrderService
    {

        //IOptions<OrderServiceOptions> _options;

        //IOptionsSnapshot<OrderServiceOptions> _options;

        IOptionsMonitor<OrderServiceOptions> _options;


        public OrderService(IOptionsMonitor<OrderServiceOptions> options)
        {
            this._options = options;

            // 监听变更.
            this._options.OnChange(options =>
            {
                Console.WriteLine("配置发生了变更!!!");
            });
        }

        public int ShowMaxOrderCount()
        {
            return this._options.CurrentValue.MaxOrderCount;
        }
    }

    public class OrderServiceOptions
    {
        [Range(30, 100)]
        public int MaxOrderCount { get; set; } = 100;
    }

    public class OrderServiceValidateOptions : IValidateOptions<OrderServiceOptions>
    {
        public ValidateOptionsResult Validate(string name, OrderServiceOptions options)
        {
            if (options == null)
            {
                return ValidateOptionsResult.Fail("参数为空");
            }
            if (options.MaxOrderCount > 100)
            {
                return ValidateOptionsResult.Fail("参数不能大于 100");
            }
            else
            {
                return ValidateOptionsResult.Success;
            }
        }
    }
}
