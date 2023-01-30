using Blog.API.Entity;
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
                    list = from list1 in list
                           group list1 by new
                           {
                               Id = list1.Id
                           } into grouplist
                           select new BlogSingle
                           {
                               Id = grouplist.Key.Id,
                               B_Title = grouplist.Select(p => p.B_Title).FirstOrDefault(),
                               B_Images = grouplist.Select(p => p.B_Title).FirstOrDefault(),
                               B_Content = grouplist.Select(p => p.B_Title).FirstOrDefault(),
                               B_Comment = grouplist.Select(p => p.B_Title).FirstOrDefault(),
                               B_Watched = grouplist.Select(p => p.B_Watched).FirstOrDefault(),
                               B_Replied = grouplist.Select(p => p.B_Replied).FirstOrDefault(),
                               Creatdate = grouplist.Select(p => p.Creatdate).FirstOrDefault(),
                               Modifydate = grouplist.Select(p => p.Modifydate).FirstOrDefault(),
                               Uid = grouplist.Select(p => p.Uid).FirstOrDefault(),
                               Tagids = grouplist.Any() ? grouplist.Select(p => p.Tagids.FirstOrDefault()).ToList() : new List<int>(),
                               TagNames = grouplist.Any() ? grouplist.Select(p => p.TagNames.FirstOrDefault()).ToList() : new List<string>()
                           };
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
