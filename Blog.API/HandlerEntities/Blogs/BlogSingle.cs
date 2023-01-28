using Blog.API.Entity.Models;
using Blog.API.Entity;
using MediatR;

namespace Blog.API.HandlerEntities.Blogs
{
    public class BlogSingle : BlogModel, IRequest<BaseResult>
    {
    }
}
