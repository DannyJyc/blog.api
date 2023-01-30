using Blog.API.Entity.Models;
using Blog.API.Entity;
using Blog.API.HandlerEntities.Blogs;
using MediatR;

namespace Blog.API.Handlers.BlogHandler
{
    public class AddBlog : IRequestHandler<BlogInsert, BaseResult>
    {
        private readonly EFCoreContext _context;
        public AddBlog(EFCoreContext context)
        {
            _context = context;
        }
        public Task<BaseResult> Handle(BlogInsert request, CancellationToken cancellationToken)
        {
            BaseResult baseResult = new();
            baseResult.code = 200;
            baseResult.message = "添加成功";
            try
            {
                BlogModel b = request;
                _context.Blogs.Add(b);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                baseResult.code = 500;
                baseResult.message = "服务器出现异常";
                baseResult.data = ex;
            }
            return Task.FromResult(baseResult);
        }
    }
}
