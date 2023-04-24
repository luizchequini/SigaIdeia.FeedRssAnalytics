using Microsoft.AspNetCore.Mvc;
using SigaIdeia.FeedRssAnalytics.Infra.Data.Orm;
using SigaIdeia.FeedRssAnalyticsApi.DTOs;
using System.Globalization;

namespace SigaIdeia.FeedRssAnalyticsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        readonly CultureInfo _culture = new("en-US");
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private static readonly object _lock = new();

        public AnalyticsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetCategories/{authorId}")]
        public IQueryable<Categories> GetCategories(string authorId)
        {
            return from x in _context.ArticleMatrices?.Where(x => x.AuthorId == authorId).GroupBy(x => x.Category)
                   select new Categories
                   {
                       Name = x.FirstOrDefault().Category,
                       Count = x.Count()
                   };
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IQueryable<Authors> GetAuthors()
        {
            return _context.ArticleMatrices.GroupBy(x => x.AuthorId).Select(group => new Authors
            {
                AuthorId = group.FirstOrDefault().AuthorId,
                Author = group.FirstOrDefault().Author,
                Count = group.Count()
            })
            .OrderBy(group => group.Author);
        }
    }
}
