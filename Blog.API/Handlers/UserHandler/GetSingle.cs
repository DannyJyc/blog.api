using Blog.API.Entity.Models;
using Blog.API.Entity;
using Blog.API.HandlerEntities.Users;
using MediatR;

namespace Blog.API.Handlers.UserHandler
{
    public class GetSingle : IRequestHandler<UserSingle, User>
    {
        private readonly EFCoreContext _context;
        public GetSingle(EFCoreContext context) //创造一个构造函数 当引用_context时就会引用这函数
        {
            _context = context;
        }
        public Task<User> Handle(UserSingle request, CancellationToken cancellationToken)
        {
            try
            {
                var list = from users in _context.Users
                           where users.Id == request.Id
                           select users;
                if (list.Any())
                {
                    return Task.FromResult(list.FirstOrDefault());
                }
                return Task.FromResult(new User());
            }
            catch (Exception ex)
            {
                return Task.FromResult(new User());
            }

        }
    }
}
