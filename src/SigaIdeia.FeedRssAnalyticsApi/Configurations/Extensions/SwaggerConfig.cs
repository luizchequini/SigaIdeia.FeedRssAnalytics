using Microsoft.OpenApi.Models;
using System.Reflection;

namespace SigaIdeia.FeedRssAnalyticsApi.Configurations.Extensions
{
    // O do SOLID Open-Closed Principle
    /*
     * Toda classe deve esta aberta para extensão e fechada para modificação
     */
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Web Scraping com Asp.Nete Core 7 & Angular 15",
                    Description = "Esta API serve recursos para consumo de Feed RSS Web Scraping",
                    Contact = new OpenApiContact()
                    {
                        Name = "Luiz Chequini",
                        Email = "luizchequini@gmail.com"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                    TermsOfService = new Uri("https://luizchequini.com")
                });

                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

                #region: Security
                //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //    {
                //        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                //        Name = "Authorization",
                //        Scheme = "Bearer",
                //        BearerFormat = "JWT",
                //        In = ParameterLocation.Header,
                //        Type = SecuritySchemeType.ApiKey
                //    });

                //    c.AddSecurityDefinition(new OpenApiSecurityRequirement
                //    {
                //        {
                //            new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                },
                //                new string[] {}
                //            }
                //        }
                //    });
                #endregion

            });

            return services;
        }

        public static IApplicationBuilder useSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Siga Ideia | Web Scraping - v1");
            });

            return app;
        }
    }
}
