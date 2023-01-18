namespace Blog.API.Entity.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string B_Title { get; set; }
        public string B_Images { get; set; }
        public string B_Content { get; set; }
        public string B_Comment { get; set; }
        public int B_Watched { get; set; }
        public int B_Replied { get; set; }
        public DateTime Creatdate { get; set; }
        public DateTime Modifydate { get; set; }
        public int Uid { get; set; }
        public List<int> Tagids { get; set; }
    }
}
