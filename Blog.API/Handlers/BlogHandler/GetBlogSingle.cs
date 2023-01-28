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
                           where blog.Id == request.Id
                           select blog;
                if (list.Any())
                {
                    baseResult.data= list.FirstOrDefault();
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
