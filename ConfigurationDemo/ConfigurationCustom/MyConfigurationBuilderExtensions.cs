using ConfigurationCustom;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

// 将这个暴露给使用者，而隐藏内部实现
namespace Microsoft.Extensions.Configuration  // 
{
    public static class MyConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new MyConfigurationSource());
            return builder;
        }
    }
}
