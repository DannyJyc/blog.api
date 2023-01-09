using Blog.API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Blog.API.JwtBearer.Filter
{
    public class JwtAuthorizeFilter : Attribute,IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //返回结果之后
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = new BaseResult<IActionResult>
            {
                code = 1,
                message= string.Empty,
                data = context.Result
            };
            //返回结果之前
            context.Result = new ContentResult
            {
                // 返回状态码设置为200，表示成功
                StatusCode = (int)HttpStatusCode.OK,
                // 设置返回格式
                ContentType = "application/json;charset=utf-8",
                Content = JsonConvert.SerializeObject(result)
            };
        }
    }
}
