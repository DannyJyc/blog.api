using Blog.API.Entity;
using Blog.API.Entity.Models;
using MediatR;

namespace Blog.API.HandlerEntities.Users
{
    public class UserList : User,IRequest<BaseResult>
    {
    }
}
