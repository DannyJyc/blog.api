using MediatR;
using Microsoft.EntityFrameworkCore;
using Blog.API.Entity;
using Blog.API.Entity.Models;
using Blog.API.Helper;
using System;

namespace Blog.API.Handlers.UserHandler
{
    public class GetUser : IRequestHandler<User, List<User>>
    {
        private readonly EFCoreContext _context;
        public GetUser(EFCoreContext context) //创造一个构造函数 当引用_context时就会引用这函数
        {
            _context = context;
        }
        public Task<List<User>> Handle(User request, CancellationToken cancellationToken)
        {
            var list = from users in _context.Users
                       select users;
            return Task.FromResult(list.ToList());
        }
    }

}
