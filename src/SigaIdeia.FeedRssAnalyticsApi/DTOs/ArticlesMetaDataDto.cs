using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Services;

namespace SigaIdeia.FeedRssAnalyticsApi.DTOs
{
    public class ArticlesMetaDataDto
    {
        public int TotalResults { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }

        public ArticlesMetaDataDto(PagedResultFeed<ArticleMatrix> articles)
        {
            TotalResults = articles.TotalResults;
            PageIndex = articles.PageIndex;
            PageSize = articles.PageSize;
            TotalPages = articles.TotalPages;
            HasPrevious = articles.HasPrevious;
            HasNext = articles.HasNext;
        }
    }
}
