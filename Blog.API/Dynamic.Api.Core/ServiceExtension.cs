using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.API.Dynamic.Api.Core
{
    /// <summary>
    /// 为了将验证控制器的方法加入到.NET的框架中，ConfigureApplicationPartManager扩展方法
    /// <!--这玩意在IMvcBuilder和IMvcCoreBuilder都有。。。虽然只有第一个被引用了严谨点就都写上了-->
    /// </summary>
    /// <remarks>https://learn.microsoft.com/zh-CN/aspnet/core/mvc/advanced/app-parts?view=aspnetcore-6.0</remarks>
    public static class ServiceExtension
    {
        public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            // 添加 ApiFeatureProvider，判断某个类是否为控制器
            builder.ConfigureApplicationPartManager(applicationPartManager =>
            {
                applicationPartManager.FeatureProviders.Add(new ApiFeatureProvider());
            });
            // 添加注册自定义的路由规则
            // https://learn.microsoft.com/zh-cn/aspnet/core/mvc/controllers/routing?view=aspnetcore-7.0
            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new ApiConvention());
            });

            return builder;
        }

        public static IMvcCoreBuilder AddDynamicWebApi(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureApplicationPartManager(applicationPartManager =>
            {
                applicationPartManager.FeatureProviders.Add(new ApiFeatureProvider());
            });

            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new ApiConvention());
            });

            return builder;
        }
    }
}
