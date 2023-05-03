using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
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

        public async Task<IEnumerable<ArticleMatrix>> GetCategoryAndOrTitle(string? category = null, string? title = null)
        {
            var data = new List<ArticleMatrix>();
            var source = _context.ArticleMatrices?.AsQueryable();

            if(category.IsNullOrEmpty() && !title.IsNullOrEmpty()) 
            {
                data = await source.Where(x => x.Title.Contains(title)).ToListAsync();
            }
            else if(!category.IsNullOrEmpty() &&  !title.IsNullOrEmpty())
            {
                data = await source.Where(x => x.Category.Contains(category) && x.Title.Contains(title)).ToListAsync();
            }
            else if(!category.IsNullOrEmpty() && title.IsNullOrEmpty())
            {
                data = await source.Where(x => x.Category.Contains(category)).ToListAsync();
            }
            else
            {
                data = await source.ToListAsync();
            }

            return await Task.FromResult(data);
        }
    }
}
