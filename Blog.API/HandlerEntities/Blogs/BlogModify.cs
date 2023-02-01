using Blog.API.Entity.Models;
using Blog.API.Entity;
using MediatR;

namespace Blog.API.HandlerEntities.Blogs
{
    public class BlogInsert : BlogModel, IRequest<BaseResult>
    {
        /// <summary>
        /// 标签名字
        /// </summary>
        public List<string> TagNames { get; set; }
    }
}
