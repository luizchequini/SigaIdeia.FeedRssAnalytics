﻿using AutoMapper;
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

        public FilterController(IArticleMatrixRepository articleMatrixRepository, IMapper mapper)
        {
            _articleMatrixRepository = articleMatrixRepository;
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
        [ProducesResponseType(typeof(IQueryable<ArticleMatrixDto>), 200)]
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
        [ProducesResponseType(typeof(PagedResultFeed<ArticleMatrix>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetCategoryAndTitle(
            [FromQuery] int pageIndex = 1, int pageSize = 10, string? category = null, string? title = null)
        {
            var artigos = await _articleMatrixRepository.GetCategoryAndTitle(pageIndex, pageSize, category, title);

            var metaData = new ArticlesMetaDataDto(artigos);

            GenericResponseHeader(metaData);

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
        [ProducesResponseType(typeof(PagedResultFeed<ArticleMatrix>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetCategoryAndOrTitle(
            [FromQuery] int pageIndex = 1, int pageSize = 10, string? category = null, string? title = null)
        {
            var artigos = await _articleMatrixRepository.GetCategoryAndOrTitle(pageIndex, pageSize, category, title);

            var metaData = new ArticlesMetaDataDto(artigos);

            GenericResponseHeader(metaData);

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
        [ProducesResponseType(typeof(PagedResultFeed<ArticleMatrix>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<PagedResultFeed<ArticleMatrix>>> GetFilterByYear(
            [FromQuery] int pageIndex = 1, int pageSize = 10, int? query = null)
        {
            var artigos = await _articleMatrixRepository.GetFilterByYear(pageIndex, pageSize, query);

            var metaData = new ArticlesMetaDataDto(artigos);

            GenericResponseHeader(metaData);

            return Ok(artigos);
        }

        private void GenericResponseHeader(ArticlesMetaDataDto metadata)
        {
            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("x-Pagination", JsonConvert.SerializeObject(metadata));
            Response.Headers.Add("x-Pagination-Result", $"Voce esta na pagina {metadata.PageIndex} de {metadata.TotalPages} com total de {metadata.TotalResults} registros");

        }
    }
}
