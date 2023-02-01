using Blog.API.Entity;
using Blog.API.Entity.Models;
using Blog.API.Entity.StatusControlExpand;
using Blog.API.HandlerEntities.Blogs;
using MediatR;

namespace Blog.API.Handlers.BlogHandler
{
    public class UpdateBlog : IRequestHandler<BlogModify, BaseResult>
    {
        private readonly EFCoreContext _context;
        public UpdateBlog(EFCoreContext context)
        {
            _context = context;
        }

        public Task<BaseResult> Handle(BlogModify request, CancellationToken cancellationToken)
        {
            BaseResult baseResult = new();
            baseResult.code = 200;
            baseResult.message = "修改成功";
            try
            {
                var b = _context.Blogs.First(p => p.Id == request.Id);
                _context.Modify(b, new
                {
                    B_Title = request.B_Title,
                    B_Images = request.B_Images,
                    B_Content = request.B_Content,
                    B_Comment = request.B_Comment,
                    Modifydate = DateTime.Now,
                    State = request.State
                });
                _context.Blog_Tags.RemoveRange(from bt in _context.Blog_Tags
                                               where bt.Bid == request.Id
                                               select bt);
                _context.SaveChanges();
                foreach (var item in request.TagNames)
                {
                    // 添加标签
                    Tag tag = new Tag
                    {
                        T_Name = item.ToLower(),
                        Creatdate = DateTime.Now
                    };
                    _context.Tags.Add(tag);
                    _context.SaveChanges();
                    // 添加绑定关系
                    Blog_Tag blog_tag = new Blog_Tag
                    {
                        Bid = b.Id,
                        Tid = tag.Id
                    };
                    _context.Blog_Tags.Add(blog_tag);
                    _context.SaveChanges();
                }
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
