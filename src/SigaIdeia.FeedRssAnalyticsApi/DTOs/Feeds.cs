namespace SigaIdeia.FeedRssAnalyticsApi.DTOs
{
    public class Feeds
    {
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? FeedType { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime PubDate { get; set; } = DateTime.Now;

        public Feeds()
        {
            Link = string.Empty;
            Title = string.Empty;
            FeedType = string.Empty;
            Author = string.Empty;
            Content = string.Empty;
            PubDate = DateTime.Today;
        }
    }
}
