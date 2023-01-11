﻿using Blog.API.Entity.Models;
using Blog.API.Helper;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Blog.API.JwtBearer
{
    public class JwtProvider
    {
        public string Generate(User user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(GetTokenKeyHelper.GetTokenKey());
            var token = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub",user.Id.ToString()),
                    new Claim("name",user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256Signature)
            });

            return handler.WriteToken(token);
        }

        public int? Validate(string token)
        {
            if (token == null)
                return null;

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

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
