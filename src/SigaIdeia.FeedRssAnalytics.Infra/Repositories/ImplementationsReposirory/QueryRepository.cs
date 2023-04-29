using Microsoft.EntityFrameworkCore;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalytics.Infra.Data.Orm;

namespace SigaIdeia.FeedRssAnalytics.Infra.Repositories.ImplementationsReposirory
{
    public class QueryRepository : IQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public QueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Authors>? GetAuthors()
        {
            return _context.ArticleMatrices?.GroupBy(x => x.AuthorId).Select(group => new Authors
            {
                AuthorId = group.FirstOrDefault().AuthorId,
                Author = group.FirstOrDefault().Author,
                Count = group.Count()
            })
            .OrderBy(group => group.Author);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByAuthorId(string authorId)
        {
            var retval = await _context.ArticleMatrices
                 .Where(x => x.AuthorId == authorId)
                 .GroupBy(x => x.Category)
                 .Select(group => new Category
                 {
                     Name = group.FirstOrDefault().Category,
                     Count = group.Count()
                 }).ToListAsync();

            return retval;
        }
    }
}
