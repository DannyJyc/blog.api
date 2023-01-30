using Blog.API.Entity.Models;
using MediatR;

namespace Blog.API.HandlerEntities.Users
{
    public class UserSingle : User, IRequest<User>
    {
    }
}
