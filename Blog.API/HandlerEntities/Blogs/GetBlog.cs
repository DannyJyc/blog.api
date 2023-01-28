using Blog.API.Entity;
using Blog.API.Entity.Models;
using MediatR;

namespace Blog.API.HandlerEntities.Blogs
{
    public class GetBlog : BlogModel, IRequest<BaseResult>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 页数据量
        /// </summary>
        public int pageSize { get; set; }
    }
}
