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
                BlogModel b = new BlogModel
                {
                    B_Title = request.B_Title,
                    B_Images = request.B_Images,
                    B_Content = request.B_Content,
                    B_Comment = request.B_Comment,
                    B_Watched = request.B_Watched,
                    B_Replied = request.B_Replied,
                    Creatdate = DateTime.Now,
                    Modifydate = DateTime.Now,
                    Uid = request.Uid
                };
                _context.Blogs.Add(b);
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
