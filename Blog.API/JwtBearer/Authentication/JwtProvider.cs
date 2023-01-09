using Blog.API.Entity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.API.JwtBearer.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions jwtOptions;
        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public string Generate(User user)
        {
            var claims = new Claim[] {
                new(JwtRegisteredClaimNames.Sub,user.Id.ToString())
            };
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecreKey)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwtOptions.Issuer, jwtOptions.Audience, claims,null,DateTime.UtcNow.AddHours(2),null);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
