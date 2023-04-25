using SigaIdeia.FeedRssAnalytics.Domain.Entities;

namespace SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository
{
    public interface IQueryRepository
    {
        IQueryable<Category> GetCategoriesByAuthorId(string authorId);
        IQueryable<Authors> GetAuthors();
    }
}
