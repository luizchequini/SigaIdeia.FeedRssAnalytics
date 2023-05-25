using Microsoft.EntityFrameworkCore;
using SigaIdeia.FeedRssAnalytics.CoreShare.Configurations;
using SigaIdeia.FeedRssAnalytics.Domain.Repositories.AbstractRepository;
using SigaIdeia.FeedRssAnalytics.Infra.Data.Orm;
using SigaIdeia.FeedRssAnalytics.Infra.Repositories.ImplementationsReposirory;
using SigaIdeia.FeedRssAnalyticsApi.Configurations.AutoMappers;
using SigaIdeia.FeedRssAnalyticsApi.Configurations.Extensions;
using SigaIdeia.FeedRssAnalyticsApi.Configurations.FiltersAndAtttributes;
using System.Net;

namespace SigaIdeia.FeedRssAnalyticsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.
           
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

            builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

            builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<GlobalProducesResponseTypeFilter>();
                opt.Filters.Add(new GlobalProducesRonponseTypeAttributes(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest));
                opt.Filters.Add(new GlobalProducesRonponseTypeAttributes(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized));
                opt.Filters.Add(new GlobalProducesRonponseTypeAttributes(typeof(ErrorResponse), (int)HttpStatusCode.NotFound));
                opt.Filters.Add(new GlobalProducesRonponseTypeAttributes(typeof(ErrorResponse), (int)HttpStatusCode.MethodNotAllowed));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerConfiguration();


            builder.Services.AddScoped<IQueryRepository, QueryRepository>();
            builder.Services.AddScoped<IQueryADORepository, QueryADORepository>();
            builder.Services.AddScoped<IArticleMatrixRepository, ArticleMatrixRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();

                app.useSwaggerConfiguration();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}