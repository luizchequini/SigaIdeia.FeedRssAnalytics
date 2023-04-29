using SigaIdeia.FeedRssAnalytics.Domain.Entities;

namespace SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository
{
    public interface IQueryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesByAuthorId(string authorId);
        IQueryable<Authors> GetAuthors();
    }
}
