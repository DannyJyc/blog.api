using Blog.API.Dynamic.Api.Core;
using Blog.API.Dynamic.Api.Core.Attributes;
using Blog.API.Entity;
using Blog.API.Entity.Models;
using Blog.API.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Blog.API.JwtBearer.Filter;

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
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<BaseResult> Login(User user)
        {
            if(!string.IsNullOrEmpty(user.Username)&&!string.IsNullOrEmpty(user.Password)) {
                var result = _mediator.Send(user);
                return result;
            }
            else
            {
                return Task.FromResult( new BaseResult { code = 201,message = "请输入用户名和密码"} );
            }

        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [JwtAuthorizeFilter]
        public Task<BaseResult> GetAllUsers()
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
