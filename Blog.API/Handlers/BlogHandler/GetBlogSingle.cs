using Blog.API.Entity.Models;
using Blog.API.Entity;
using Blog.API.HandlerEntities.Users;
using MediatR;
using Blog.API.HandlerEntities.Blogs;

namespace Blog.API.Handlers.BlogHandler
{
    public class GetBlogSingle : IRequestHandler<BlogSingle, BaseResult>
    {
        private readonly EFCoreContext _context;
        public GetBlogSingle(EFCoreContext context) //创造一个构造函数 当引用_context时就会引用这函数
        {
            _context = context;
        }
        public Task<BaseResult> Handle(BlogSingle request, CancellationToken cancellationToken)
        {
            BaseResult baseResult = new();
            baseResult.code = 200;
            baseResult.message = "查询成功";
            try
            {
                var list = from blog in _context.Blogs
                           join bt in _context.Blog_Tags on blog.Id equals bt.Bid
                           join t in _context.Tags on bt.Tid equals t.Id into Tags
                           where blog.Id == request.Id
                           select new BlogSingle
                           {
                               Id = blog.Id,
                               B_Title = blog.B_Title,
                               B_Images = blog.B_Images,
                               B_Content = blog.B_Content,
                               B_Comment = blog.B_Comment,
                               B_Watched = blog.B_Watched,
                               B_Replied = blog.B_Replied,
                               Creatdate = blog.Creatdate,
                               Modifydate = blog.Modifydate,
                               Uid = blog.Uid,
                               Tagids = Tags.Any() ? Tags.Select(p => p.Id).ToList() : new List<int>(),
                               TagNames = Tags.Any() ? Tags.Select(p => p.T_Name).ToList() : new List<string>()
                           };
                if (list.Any())
                {
                    baseResult.data = list.FirstOrDefault();
                    return Task.FromResult(baseResult);
                }
                baseResult.code = 404;
                baseResult.message = "没有查询到对应博客~！";
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
