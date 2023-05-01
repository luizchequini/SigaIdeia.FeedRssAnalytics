using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SigaIdeia.FeedRssAnalytics.Domain.Entities;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;

namespace SigaIdeia.FeedRssAnalytics.Infra.Repositories.ImplementationsReposirory
{
    public class QueryADORepository : IQueryADORepository
    {
        private readonly IConfiguration _configuration;
        public QueryADORepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<IEnumerable<Authors>> GetAuthors()
        {
            var queryExecuted = ExecuteQuery(
                "SELECT AuthorId, Author, COUNT(*) as Count FROM ArticleMatrices GROUP BY AuthorId, Author ORDER BY Author",
                null,
                reader => new Authors
                {
                    AuthorId = reader["AuthorId"].ToString(),
                    Author = reader["Author"].ToString(),
                    Count = (int)reader["Count"],
                });

            return await Task.FromResult(queryExecuted);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByAuthorId(string authorId)
        {
            var queryExecuted = ExecuteQuery(
            "SELECT Category, COUNT(*) as Count FROM ArticleMatrices WHERE AuthorId = @AuthorId GROUP BY Category",
            new SqlParameter("@AuthorId", authorId),
            reader => new Category
            {
                Name = reader["Category"].ToString(),
                Count = (int)reader["Count"],
            });

            return await Task.FromResult(queryExecuted);
        }

        public async Task<IEnumerable<ArticleMatrix>> GetAllByArticlesByAuthorId(string authorId)
        {
            var queryExecuted = ExecuteQuery(
            "SELECT * FROM ArticleMatrices WHERE AuthorId = @AuthorId ORDER BY PubDate DESC",
            new SqlParameter("@AuthorId", authorId),
            reader => new ArticleMatrix
            {
                Id = (int)reader["Id"],
                AuthorId = reader["AuthorId"].ToString(),
                Author = reader["Author"].ToString(),
                Link = reader["Link"].ToString(),
                Title = reader["Title"].ToString(),
                Type = reader["Type"].ToString(),
                Category = reader["Category"].ToString(),
                Views = reader["Views"].ToString(),
                ViewsCount = (decimal)reader["ViewsCount"],
                Likes = (int)reader["Likes"],
                PubDate = (DateTime)reader["PubDate"]
            });

            return await Task.FromResult(queryExecuted);
        }

        #region: CONSULTA ADO.NET PURO - PRAMETRIZADA E GENÉRICA
        private IEnumerable<T> ExecuteQuery<T>(string query, SqlParameter? sqlParameter, Func<SqlDataReader, T> map)
        {
            var result = new List<T>();
            var _connectionString = _configuration.GetConnectionString("ConnStr");

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        if (sqlParameter != null)
                        {
                            command.Parameters.Add(sqlParameter);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(map(reader));
                            }
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion
    }
}
