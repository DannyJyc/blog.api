using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Blog.API.Dynamic.Api.Core
{
    public class ApiFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 判断是不是控制器
        /// <!--只要实现了我们自定义的接口，加上符合一些规则的话就认定它是控制器（之后陆续完善这部分规则）-->
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (typeof(IService).IsAssignableFrom(typeInfo))
            {
                if (!typeInfo.IsInterface &&
                    !typeInfo.IsAbstract &&
                    !typeInfo.IsGenericType &&
                    typeInfo.IsPublic)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
