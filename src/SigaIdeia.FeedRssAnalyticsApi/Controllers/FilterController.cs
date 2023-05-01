using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalyticsApi.DTOs;

namespace SigaIdeia.FeedRssAnalyticsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IArticleMatrixRepository _articleMatrixRepository;
        private readonly IMapper _mapper;

        public FilterController(IArticleMatrixRepository articleMatrixRepository, IMapper mapper)
        {
            _articleMatrixRepository = articleMatrixRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetDistinctCategory")]
        [ProducesResponseType(typeof(IQueryable<ArticleMatrixDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public IQueryable<Category> GetDistinctCategory()
        {
            return _articleMatrixRepository.GetDistinctCategory();
        }
    }
}
