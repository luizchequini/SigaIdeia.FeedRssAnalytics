using Microsoft.AspNetCore.Mvc;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Infra.Data.Orm;
using SigaIdeia.FeedRssAnalyticsApi.DTOs;

namespace SigaIdeia.FeedRssAnalyticsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FilterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetDistinctCategory")]
        [ProducesResponseType(typeof(IQueryable<ArticleMatrixDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public IQueryable<Category> GetDistinctCategory() 
        {
            return from x in _context.ArticleMatrices?.GroupBy(x => x.Category)
                   select new Category
                   {
                       Name = x.FirstOrDefault().Category,
                       Count = x.Count()
                   };
        }
    }
}
