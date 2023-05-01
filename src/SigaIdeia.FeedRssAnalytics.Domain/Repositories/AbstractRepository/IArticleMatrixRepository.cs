using SigaIdeia.FeedRssAnalytics.Domain.Entities;

namespace SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository
{
    public interface IArticleMatrixRepository
    {
        IQueryable<Category> GetDistinctCategory();

        Task<IEnumerable<ArticleMatrix>> GetCategoryAndOrTitle();
    }
}
