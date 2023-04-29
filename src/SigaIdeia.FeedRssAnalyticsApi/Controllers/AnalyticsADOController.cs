using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalyticsApi.DTOs;

namespace SigaIdeia.FeedRssAnalyticsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnalyticsADOController : ControllerBase
    {
        private readonly IQueryADORepository _queryADORepository;
        private readonly IMapper _mapper;
        private object iEnumAuthor;

        public AnalyticsADOController(IQueryADORepository queryADORepository, IMapper mapper)
        {
            _queryADORepository = queryADORepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetCategory/{authorId}")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategory(string? authorId)
        {
            if(string.IsNullOrWhiteSpace(authorId))
            {
                return BadRequest(string.Empty);
            }

            var author = await _queryADORepository.GetCategoriesByAuthorId(authorId);

            if(author == null)
            {
                return NotFound();
            }

            var categories = _mapper.Map<IEnumerable<CategoryDto>>(author);   
            
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetAuthors")]
        [ProducesResponseType(typeof(IEnumerable<AuthorsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<AuthorsDto>>> GetAuthors()
        {
            var iEnumerableAuthor = await _queryADORepository.GetAuthors();

            return Ok(_mapper.Map<IEnumerable<AuthorsDto>>(iEnumerableAuthor));
        }
    }
}
