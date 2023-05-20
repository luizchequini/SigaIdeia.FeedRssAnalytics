using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Services;

namespace SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository
{
    public interface IArticleMatrixRepository
    {
        IQueryable<Category> GetDistinctCategory();

        Task<PagedResultFeed<ArticleMatrix>> GetCategoryAndOrTitle(int pageIndex, int pageSize, string? category = null, string? title = null);
        
        Task<PagedResultFeed<ArticleMatrix>> GetCategoryAndTitle(int pageIndex, int pageSize, string? category = null, string? title = null);
    }
}
