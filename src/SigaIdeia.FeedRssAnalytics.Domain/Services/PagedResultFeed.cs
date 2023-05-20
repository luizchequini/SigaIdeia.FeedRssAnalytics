namespace SigaIdeia.FeedRssAnalytics.Domain.Services
{
    public class PagedResultFeed<T> where T : class
    {
        public IEnumerable<T>? Data { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Query { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}
