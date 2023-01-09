using Blog.API.Entity.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Blog.API.JwtBearer
{
    public class JwtProvider
    {
        public string Generate(User user)
        {
            var rsaKey = RSA.Create();
            rsaKey.ImportRSAPrivateKey(File.ReadAllBytes("key"),out _);
            var handler = new JsonWebTokenHandler();
            var key = new RsaSecurityKey(rsaKey);
            string token = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub",user.Id.ToString()),
                    new Claim("name",user.Username)
                }),
                SigningCredentials = new SigningCredentials(key,SecurityAlgorithms.RsaSha256)
            });

            return token;
        }
    }
}
