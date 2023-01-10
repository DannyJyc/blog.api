using Blog.API.Entity.Models;
using Blog.API.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 新增测试
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public string Create(User user)
        {
            return $"创建了：{user.Username} ";
        }
        [HttpGet("Delete")]
        public string Delete(int id)
        {
            return $"ID：{id} 已删除";
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public Task<BaseResult> Login(User user)
        {
            if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
            {
                var result = _mediator.Send(user);
                return result;
            }
            else
            {
                return Task.FromResult(new BaseResult { code = 201, message = "请输入用户名和密码" });
            }

        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAllUsers")]
        public Task<BaseResult> GetAllUsers()
        {
            var result = _mediator.Send(new User());
            return result;
        }
        [HttpPost("Update")]
        public string Update(User user)
        {
            return $" ID：{user.Id} 的名字改成了 {user.Username}";
        }
        [HttpGet("GetTest")]
        public string GetTest(int id)
        {
            return $"TEST你输入的 ID 是：{id}";
        }
    }
}
