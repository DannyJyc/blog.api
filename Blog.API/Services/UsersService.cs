using Blog.API.Dynamic.Api.Core;
using Blog.API.Dynamic.Api.Core.Attributes;
using Blog.API.Entity.Models;
using Blog.API.Helper;
using MediatR;
using System;

namespace Blog.API.Services
{
    public class UsersService : IService
    {
        private readonly IMediator _mediator;

        public UsersService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 新增测试
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Create(User user)
        {
            return $"创建了：{user.Username} ";
        }

        public string Delete(int id)
        {
            return $"ID：{id} 已删除";
        }

        public string Get(int id)
        {
            return $"你输入的 ID 是：{id}";
        }

        public Task<List<User>>? GetAllUsers()
        {
            var result = _mediator.Send(new User());
            return result;
        }

        public string Update(int id, User user)
        {
            return $" ID：{id} 的名字改成了 {user.Username}";
        }

        [NonDynamicAction]
        public string GetTest(int id)
        {
            return $"TEST你输入的 ID 是：{id}";
        }
    }
}
