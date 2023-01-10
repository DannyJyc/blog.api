
using Blog.API.Entity;
using Blog.API.Helper;
using Blog.API.JwtBearer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = "dannyjiang12345678";
// Add services to the container.
// 调用扩展方法
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Nuget MediatR.Extensions.Microsoft.DependencyInjection 拓展 IServiceCollection 
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//var connection = @"server=./;uid=sa;pwd=123456;Database=react_blog;MultipleActiveResultSets=true;Encrypt=False";                                                                                                
//builder.Services.AddDbContext<EFCoreContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<EFCoreContext>(options => options.UseSqlServer(AppsettingHelper.Configuration.GetSection("SQLServerConnectingString").Value));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "React.Blog API",
        Description = "一个管理React.Blog API的后台管理界面",
        Contact = new OpenApiContact
        {
            Name = "我的Gitee",
            Url = new Uri("https://gitee.com/hash-bear")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAuthentication(p =>
{
    p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(p =>
{
    p.RequireHttpsMetadata = false;
    p.SaveToken = true;
    p.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSingleton(new JwtProvider(key));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
