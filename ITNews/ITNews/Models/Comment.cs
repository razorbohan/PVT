namespace ITNews.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public News News { get; set; }
    }
}
