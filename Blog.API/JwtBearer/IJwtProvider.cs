using Blog.API.Entity.Models;

namespace Blog.API.JwtBearer
{
    public interface IJwtProvider
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        string Generate(User user);
    }
}
