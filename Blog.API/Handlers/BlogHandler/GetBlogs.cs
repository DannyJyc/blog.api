using Blog.API.Entity;
using Blog.API.HandlerEntities.Blogs;
using Blog.API.HandlerEntities.Users;
using Blog.API.JwtBearer;
using MediatR;

namespace Blog.API.Handlers.BlogHandler
{
    public class GetBlogs : IRequestHandler<GetBlog, BaseResult>
    {
        private readonly EFCoreContext _context;
        public GetBlogs(EFCoreContext context)
        {
            _context = context;
        }
        public Task<BaseResult> Handle(GetBlog request, CancellationToken cancellationToken)
        {
            BaseResult baseResult = new();
            baseResult.code = 200;
            baseResult.message = "查询成功";
            try
            {
                var list = from blog in _context.Blogs
                           join bt in _context.Blog_Tags on blog.Id equals bt.Bid into Tags
                           select new GetBlog
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
                               Tagids = Tags.Any() ? Tags.Select(p => p.Tid).ToList() : new List<int>()
                           };
                // 分页
                if (request.page > 0 && request.pageSize > 0)
                {
                    list = list.Skip((request.page - 1) * request.pageSize).Take(request.pageSize);
                }
                baseResult.data = list;
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
