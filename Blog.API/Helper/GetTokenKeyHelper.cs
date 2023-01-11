using Blog.API.Helper.AppsettingOptions;

namespace Blog.API.Helper
{
    public class GetTokenKeyHelper
    {
        private static IConfiguration _configuration;
        public GetTokenKeyHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string? GetTokenKey()
        {
            var formatSettings = _configuration.GetSection("JwtBearer").Get<JwtBearerOptions>();
            return formatSettings?.Key;
        }
    }
}
