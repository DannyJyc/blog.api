using MediatR;

namespace Blog.API.Entity.Models
{
    public class User : IRequest<List<User>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Creatdate { get; set; }
    }
}
