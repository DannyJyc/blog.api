﻿using MediatR;

namespace Blog.API.Entity.Models
{
    public class User : IRequest<BaseResult>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Creatdate { get; set; }
    }
}
