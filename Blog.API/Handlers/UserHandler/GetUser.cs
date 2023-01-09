using MediatR;
using Microsoft.EntityFrameworkCore;
using Blog.API.Entity;
using Blog.API.Entity.Models;
using Blog.API.Helper;
using System;
using Microsoft.IdentityModel.Tokens;
using Blog.API.JwtBearer;

namespace Blog.API.Handlers.UserHandler
{
    public class GetUser : IRequestHandler<User, BaseResult>
    {
        private readonly EFCoreContext _context;
        private readonly IJwtProvider _jwtProvider;
        public GetUser(EFCoreContext context) //创造一个构造函数 当引用_context时就会引用这函数
        {
            _context = context;
        }
        public Task<BaseResult> Handle(User request, CancellationToken cancellationToken)
        {
            BaseResult baseResult = new();
            baseResult.code = 200;
            baseResult.message = "查询成功";

            try
            {
                var list = from users in _context.Users
                           select users;
                if (list.Any())
                {
                    if (!string.IsNullOrEmpty(request.Username))
                    {
                        User singleUser = new();
                        list = from users in list
                               where users.Username == request.Username && users.Password == request.Password
                               select users;
                        if (list.Any())
                        {
                            singleUser = list.FirstOrDefault();
                            baseResult.data = singleUser;
                            string token = _jwtProvider.Generate(singleUser);
                            baseResult.token = token;
                        }
                        else
                        {
                            baseResult.code = 404;
                            baseResult.message = "用户名或密码输入有误";
                        }
                        return Task.FromResult(baseResult);
                    }
                }
                baseResult.data = list;
                return Task.FromResult(baseResult);
            }
            catch (Exception ex)
            {
                baseResult.code = 500;
                baseResult.message = "服务器出现异常";
                baseResult.data = ex;
                return Task.FromResult(baseResult);
            }

        }
    }

}
