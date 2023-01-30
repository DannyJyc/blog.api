using Blog.API.Entity;
using Blog.API.Entity.Models;
using MediatR;

namespace Blog.API.HandlerEntities.Blogs
{
    public class BlogList : BlogModel, IRequest<BaseResult>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 页数据量
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string tname { get; set; }
        /// <summary>
        /// 标签名字
        /// </summary>
        public List<string> TagNames { get; set; }
    }
}
