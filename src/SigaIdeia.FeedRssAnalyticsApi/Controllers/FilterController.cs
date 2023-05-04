using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalytics.Domain.Services;
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

        [HttpGet]
        [Route("GetCategoryAndOrTitle")]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetCategoryAndOrTitle(
                    [FromQuery] int pageIndex, int pageSize, string? category = null, string? title = null)
        {
            var artigos = await _articleMatrixRepository.GetCategoryAndOrTitle(pageIndex, pageSize, category, title);

            var metaData = new
            {
                artigos.TotalPages,
                artigos.PageIndex,
                artigos.PageSize,
                artigos.HasPrevious,
                artigos.HasNext,
                artigos.TotalResults
            };

            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metaData));
            Response.Headers.Add("x-Pagination-Result", $"Retornando {artigos.TotalResults} Registros do Banco de Dados");

            return Ok(artigos);
        }
    }
}
