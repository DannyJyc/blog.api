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
        private readonly IMediator _mediator;
        public JwtMiddleware(RequestDelegate next, IMediator mediator)
        {
            _next = next;
            _mediator = mediator;
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
                context.Items["User"] = null;

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
                var result = _mediator.Send(new UserSingle { Id = userId });
                context.Items["User"] = result;
            }
            catch
            {
                context.Items["User"] = null;
            }
        }
    }
}
