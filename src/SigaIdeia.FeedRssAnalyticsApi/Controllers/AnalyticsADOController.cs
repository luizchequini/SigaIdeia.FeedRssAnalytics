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

        /// <summary>
        /// Resumo analitico da Categoria
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns>
        /// Resumo analitico da Categoria</returns>
        /// <remarks>
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "name": "Nome da Categoria",
        ///         "count": "Quantidade de Post na Categoria"
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("GetCategory/{authorId}")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
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


        /// <summary>
        /// Resumo de agrupamento de Post por Autor
        /// </summary>
        /// <returns>
        /// Resumo um agrupamento de Post por Autor
        /// </returns>
        /// <remarks>
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "authoId": "Id do Autor",
        ///         "autho": "Nome do Autor",
        ///         "count": "Quantidade de Post deste Author"
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("GetAuthors")]
        [ProducesResponseType(typeof(IEnumerable<AuthorsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<AuthorsDto>>> GetAuthors()
        {
            var iEnumerableAuthor = await _queryADORepository.GetAuthors();

            return Ok(_mapper.Map<IEnumerable<AuthorsDto>>(iEnumerableAuthor));
        }

        /// <summary>
        /// Retorna uma lista de Feeds (Artigos, Blogs, Videos e etc) do Author.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns>
        /// Retorna uma lista de Feeds (Artigos, Blogs, Videos e etc) do Author.
        /// </returns>
        /// <remarks>
        /// Retorna uma lista de Feeds (Artigos, Blogs, Videos e etc) do Author.
        /// Exemplo de Request:
        /// 
        ///     GET /Todo
        ///     {
        ///         "Id"        :"Id do artigo"
        ///         "AuthorId"  :"Id do autor"
        ///         "Author"    :"Nome do autor"   
        ///         "Link"      :"Link do artigo"
        ///         "Title"     :"Título do artigo"
        ///         "Type"      :"Typo dp artigo"
        ///         "Category"  :"Categoria do artigo"
        ///         "Views"     :"Quantidade de visualização do artigo"
        ///         "ViewsCount":"Contagem total de vizualizações"
        ///         "Likes"     :"Quantidade de curtidas"
        ///         "PubDate"   :"Data da publicação do artigo"
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("GetAll/{authorId}")]
        [ProducesResponseType(typeof(IEnumerable<ArticleMatrixDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ArticleMatrixDto>>> GetAll(string authorId)
        {
            var model = await _queryADORepository.GetAllByArticlesByAuthorId(authorId);

            var aMapper = _mapper.Map<IEnumerable<ArticleMatrixDto>>(model);

            return Ok(aMapper);
        }
    }
}
