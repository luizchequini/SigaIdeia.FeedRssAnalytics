using AutoMapper;
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
        private readonly IQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public AnalyticsController(IQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetCategory/{authorId}")]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryDto>>> GetCategory(string? authorId)
        {
            if(string.IsNullOrWhiteSpace(authorId))
            {
                return BadRequest(string.Empty);
            }

            var categories = _mapper.Map<List<CategoryDto>>(await _queryRepository.GetCategoriesByAuthorId(authorId));   
            
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetAuthors")]
        public IQueryable<Authors>? GetAuthors()
        {
            return _queryRepository.GetAuthors();
        }
    }
}
