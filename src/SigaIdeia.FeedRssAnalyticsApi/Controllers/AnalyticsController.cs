using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalyticsApi.DTOs;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Globalization;
using SigaIdeia.FeedRssAnalytics.Infra.Data.Orm;
using System.Net.Http;
using System.Diagnostics;

namespace SigaIdeia.FeedRssAnalyticsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        readonly CultureInfo culture = new("en-US");
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private static readonly object _lockObj = new object();

        private readonly IQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public AnalyticsController(IQueryRepository queryRepository, IMapper mapper, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("CreatePosts/{authorId}")]
        public async Task<bool> CreatPosts(string authorId)
        {
            authorId = "mahesh-chand";

            try
            {
                XDocument doc = XDocument.Load("https://www.c-sharpcorner.com/members/" + authorId + "/rss");

                if (doc == null)
                {
                    return false;
                }

                var entries = from item in doc.Descendants()
                              .First(i => i.Name.LocalName == "channel").Elements()
                              .Where(i => i.Name.LocalName == "item")
                              select new Feed
                              {
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,

                                  Link = (item.Elements().First(i => i.Name.LocalName == "link").Value).StartsWith("/")
                                  ? "https://www.c-sharpcorner.com" + item.Elements().First(i => i.Name.LocalName == "link").Value
                                  : item.Elements().First(i => i.Name.LocalName == "link").Value,

                                  PubDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value, culture),

                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value,

                                  FeedType = (item.Elements().First(i => i.Name.LocalName == "link").Value).ToLowerInvariant().Contains("blog")
                                  ? "Blog"
                                  : (item.Elements().First(i => i.Name.LocalName == "link").Value).ToLowerInvariant().Contains("news")
                                  ? "News"
                                  : "Article",

                                  Author = item.Elements().First(i => i.Name.LocalName == "author").Value
                              };

                var filterByYear = DateTime.Now.Year - 4;

                List<Feed> feed = entries.OrderByDescending(o => o.PubDate).Where(o => o.PubDate.Year > filterByYear).ToList();

                string urlAddress = string.Empty;
                List<ArticleMatrix> articleMatrix = new();                
                _ = int.TryParse(_configuration["ParallelTasksCount"], out int parallelTasksCount);

                Console.WriteLine("Número máximo de Theads: " + parallelTasksCount.ToString());

                
                Stopwatch cronometro = Stopwatch.StartNew();
                cronometro.Start();


                Parallel.ForEach(feed, new ParallelOptions { MaxDegreeOfParallelism = parallelTasksCount }, async feed =>
                {
                    urlAddress = feed.Link;

                    var httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(urlAddress)
                    };
                    var result = httpClient.GetAsync("").Result;

                    string strData = "";

                    if(result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        strData = result.Content.ReadAsStringAsync().Result;
                        
                        HtmlDocument htmlDocument = new();

                        htmlDocument.LoadHtml(strData);

                        ArticleMatrix articleMatrix = new()
                        {
                            AuthorId = authorId,
                            Author = feed.Author,
                            Type = feed.FeedType,
                            Link = feed.Link,
                            Title = feed.Title,
                            PubDate = feed.PubDate,
                        };

                        string category = "Videos";
                        if(htmlDocument.GetElementbyId("ImgCategory") != null)
                        {
                            category = htmlDocument.GetElementbyId("ImgCategory").GetAttributeValue("title", "");
                        }

                        articleMatrix.Category = category;
                    }

                });


                cronometro.Stop();
                Console.WriteLine("\n\n\nTempo de decorrido: " + cronometro.ElapsedMilliseconds + "Milissegundos");


                return true;
            }
            catch
            {

                return false;
            }
        }

        [HttpGet]
        [Route("GetCategory/{authorId}")]
        [ProducesResponseType(typeof(List<CategoryDto>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategory(string? authorId)
        {
            if (string.IsNullOrWhiteSpace(authorId))
            {
                return BadRequest(string.Empty);
            }

            var categories = _mapper.Map<List<CategoryDto>>(await _queryRepository.GetCategoriesByAuthorId(authorId));

            return Ok(categories);
        }

        [HttpGet]
        [Route("GetAuthors")]
        [Produces("application/json")]
        public IQueryable<Authors>? GetAuthors()
        {
            return _queryRepository.GetAuthors();
        }
    }
}
