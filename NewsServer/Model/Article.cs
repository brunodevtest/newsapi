namespace NewsServer.Model
{
    public class Article
    {
        public int id { get; set; }
        public ArticleType type { get; set; }
        public string by { get; set; }
        public string time { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }

    public enum ArticleType
    {
        job,
        story,
        comment,
        poll,
        pollopt
    }
}
