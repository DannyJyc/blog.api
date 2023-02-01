using Blog.API.Entity;
using Blog.API.HandlerEntities.Blogs;
using Blog.API.JwtBearer;
using MediatR;
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
        public Task<BaseResult> GetAll(int page, int pageSize, string tname)
        {
            var result = _mediator.Send(new BlogList() { page = page, pageSize = pageSize, tname = tname });
            return result;
        }
        /// <summary>
        /// 获取我自己发布的博客
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getMyAll")]
        [Authorize]
        public Task<BaseResult> GetMyAll(int page, int pageSize)
        {
            var userid = int.Parse(HttpContext.Items["userid"] == null ? "0" : HttpContext.Items["userid"].ToString());
            var result = _mediator.Send(new BlogList() { page = page, pageSize = pageSize, Uid = userid });
            return result;
        }
        /// <summary>
        /// 查询一条博客的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getDetails/{id}")]
        public Task<BaseResult> GetDetails(int id)
        {
            var result = _mediator.Send(new BlogSingle() { Id = id });
            return result;
        }
        /// <summary>
        /// 添加一条博客
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPost("insert")]
        [Authorize]
        public Task<BaseResult> Insert(BlogInsert blog)
        {
            var userid = int.Parse(HttpContext.Items["userid"] == null ? "0" : HttpContext.Items["userid"].ToString());
            blog.Uid = userid;
            var result = _mediator.Send(blog);
            return result;
        }
        /// <summary>
        /// 修改一条博客
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPut("update")]
        //[Authorize]
        public Task<BaseResult> Update(BlogModify blog)
        {
            var result = _mediator.Send(blog);
            return result;
        }
    }
}
