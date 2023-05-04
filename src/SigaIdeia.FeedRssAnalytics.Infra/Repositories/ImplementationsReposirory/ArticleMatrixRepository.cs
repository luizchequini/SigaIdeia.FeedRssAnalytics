using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalytics.Domain.Services;
using SigaIdeia.FeedRssAnalytics.Infra.Data.Orm;

namespace SigaIdeia.FeedRssAnalytics.Infra.Repositories.ImplementationsReposirory
{
    public class ArticleMatrixRepository : IArticleMatrixRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleMatrixRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetDistinctCategory()
        {
            return from x in _context.ArticleMatrices?.GroupBy(x => x.Category)
                   select new Category
                   {
                       Name = x.FirstOrDefault().Category,
                       Count = x.Count()
                   };
        }

        public async Task<PagedResultFeed<ArticleMatrix>> GetCategoryAndOrTitle(int pageIndex, int pageSize, string? category = null, string? title = null)
        {
            var source = _context.ArticleMatrices?.AsQueryable();

            if (!string.IsNullOrEmpty(category) || !string.IsNullOrEmpty(title))
            {
                source = source?.Where(x => (category == null || x.Category.Contains(category)) && title == null || x.Title.Contains(title));
            }

            var data = await source.ToListAsync();
            return Paginate(data, pageIndex, pageSize);
        }

        private PagedResultFeed<ArticleMatrix> Paginate(IEnumerable<ArticleMatrix> data, int pageIndex, int pageSize)
        {
            int count = data.Count();
            data = data.Skip(pageSize * (pageIndex - 1)).Take(pageSize);

            return new PagedResultFeed<ArticleMatrix>()
            {
                Data = data,
                TotalResults = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                HasPrevious = pageIndex > 1,
                HasNext = pageIndex < count - 1,
            };
        }
    }
}
