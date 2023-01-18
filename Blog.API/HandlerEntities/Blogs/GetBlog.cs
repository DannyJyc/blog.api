using Blog.API.Entity;
using Blog.API.Entity.Models;
using MediatR;

namespace Blog.API.HandlerEntities.Blogs
{
    public class GetBlog : BlogModel, IRequest<BaseResult>
    {
    }
}
