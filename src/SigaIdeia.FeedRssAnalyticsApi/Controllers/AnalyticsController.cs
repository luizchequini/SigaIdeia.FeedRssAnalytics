using Microsoft.AspNetCore.Mvc;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
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

        private readonly IQueryRepository _queryRepository;

        public AnalyticsController(ApplicationDbContext context, IConfiguration configuration, IQueryRepository queryRepository)
        {
            _context = context;
            _configuration = configuration;
            _queryRepository = queryRepository;
        }

        [HttpGet]
        [Route("GetCategoriesByAuthorId/{authorId}")]
        public IQueryable<Category> GetCategoriesByAuthorId(string authorId)
        {
            return _queryRepository.GetCategoriesByAuthorId(authorId);
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IQueryable<Authors> GetAuthors()
        {
            return _queryRepository.GetAuthors();
        }
    }
}
