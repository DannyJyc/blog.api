namespace Blog.API.Helper
{
    public class GetTokenKeyHelper
    {
        public static string? GetTokenKey()
        {
            return AppsettingHelper.Configuration.GetSection("JwtBearerOptions:Key").Value;
        }
    }
}
