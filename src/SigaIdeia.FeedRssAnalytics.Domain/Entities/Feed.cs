namespace SigaIdeia.FeedRssAnalytics.Domain.Entities
{
    public class Feed
    {
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? FeedType { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime PubDate { get; set; } = DateTime.Now;

        public Feed()
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
