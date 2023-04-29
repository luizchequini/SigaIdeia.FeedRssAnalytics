using SigaIdeia.FeedRssAnalytics.Domain.Entities;

namespace SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository
{
    public interface IQueryADORepository
    {
        Task<IEnumerable<Category>> GetCategoriesByAuthorId(string authorId);
        Task<IEnumerable<Authors>> GetAuthors();
    }
}
