namespace Blog.API.Entity
{
    public class BaseResult
    {
        /// <summary>
        /// 状态码（大体与http状态码一致）
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }

    public class BaseResult<T>
    {
        /// <summary>
        /// 状态码（大体与http状态码一致）
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
}
