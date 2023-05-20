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

        /// <summary>
        /// Get distinct Category
        /// </summary>
        /// <returns>
        /// Get distinct Category
        /// </returns>
        /// <remarks>
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "Id": "Id do Artigo",
        ///         "AuthorId": "Id do Autor",
        ///         "Author": "Nome do Author"
        ///         "Link": Link deste Post deste Author"
        ///         "Title": "Título do Post deste Author"
        ///         "Type": "Tipo de Post deste Author"
        ///         "Category": "Categoria Post deste Author"
        ///         "Views": "Quantidade de Views deste Post deste Author"
        ///         "ViewsCount": "Views Count deste Post deste Author"
        ///         "Likes": "Likes deste Post deste Author"
        ///         "PubDate": "Data de publicação deste Post"
        ///     }
        /// </remarks>
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

        /// <summary>
        /// Get Category And Title
        /// </summary>
        /// <returns>
        /// Get Category And Title
        /// </returns>
        /// <remarks>
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "name": "string",
        ///         "count": "string",
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("GetCategoryAndTitle")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetCategoryAndTitle(
            [FromQuery] int pageIndex = 1, int pageSize = 10, string? category = null, string? title = null)
        {
            var artigos = await _articleMatrixRepository.GetCategoryAndTitle(pageIndex, pageSize, category, title);

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
            Response.Headers.Add("x-Pagination-Result", $"Voce esta na pagina {artigos.PageIndex} de {artigos.TotalPages} com total de {artigos.TotalResults} registros");

            return Ok(artigos);
        }

        /// <summary>
        /// Get Category And Title
        /// </summary>
        /// <returns>
        /// Get Category And Title
        /// </returns>
        /// <remarks>
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "name": "string",
        ///         "count": "string",
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("GetCategoryAndOrTitle")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetCategoryAndOrTitle(
            [FromQuery] int pageIndex = 1, int pageSize = 10, string? category = null, string? title = null)
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
            Response.Headers.Add("x-Pagination-Result", $"Voce esta na pagina {artigos.PageIndex} de {artigos.TotalPages} com total de {artigos.TotalResults} registros");

            return Ok(artigos);
        }

        /// <summary>
        /// Get Filter By Year
        /// </summary>
        /// <returns>
        /// Get Filter By Year
        /// </returns>
        /// <remarks>
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "name": "string",
        ///         "count": "string",
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("GetFilterByYear")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetFilterByYear(
            [FromQuery] int pageIndex = 1, int pageSize = 10, int? query = null)
        {
            var artigos = await _articleMatrixRepository.GetFilterByYear(pageIndex, pageSize, query);

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
            Response.Headers.Add("x-Pagination-Result", $"Voce esta na pagina {artigos.PageIndex} de {artigos.TotalPages} com total de {artigos.TotalResults} registros");

            return Ok(artigos);
        }
    }
}
