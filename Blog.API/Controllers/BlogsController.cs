using Blog.API.Entity;
using Blog.API.Entity.Models;
using Blog.API.HandlerEntities.Blogs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// 查询所有博客
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public Task<BaseResult> GetAll()
        {
            var result = _mediator.Send(new GetBlog());
            return result;  
        }
    }
}
