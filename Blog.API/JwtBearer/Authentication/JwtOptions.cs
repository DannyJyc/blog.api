namespace Blog.API.JwtBearer.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecreKey { get; set; }
    }
}
