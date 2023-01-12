using Azure.Core;
using Blog.API.Entity;
using Blog.API.Entity.Models;
using Blog.API.HandlerEntities.Users;
using Blog.API.Helper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Blog.API.JwtBearer
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            if (token == null)
                context.Items["userid"] = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(GetTokenKeyHelper.GetTokenKey());
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "sub").Value);
                
                // 自定义中间件在加入管道时EFDBcontext上下文类并没有随着项目编译完成后注入进来所以在该中间件中无法使用
                // 会引发错误：This using can throw InvalidOperationException
                // Cannot resolve scoped service 'XXX.XXX.XXX.EFCoreContext' from root provider.
                //var list = from users in _context.Users
                //           where users.Id == userId
                //           select users;
                //if (list.Any())
                //{
                context.Items["userid"] = userId;
                //}
            }
            catch
            {
                context.Items["userid"] = null;
            }
        }
    }
}
