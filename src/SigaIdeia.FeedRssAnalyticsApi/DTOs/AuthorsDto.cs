using System.ComponentModel.DataAnnotations;

namespace SigaIdeia.FeedRssAnalyticsApi.DTOs
{
    public class AuthorsDto
    {
        [Display(Name="Id do Autor")]
        public string? AuthorId { get; set; }

        [Display(Name = "Autor")]
        public string? Author { get; set; }

        [Display(Name ="Nº de Post do Autor")]
        public int Count { get; set; }
    }
}
