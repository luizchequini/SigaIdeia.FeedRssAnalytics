using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigaIdeia.FeedRssAnalytics.Domain.Entities
{
    public class ArticleMatrix
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Autor Id")]
        public string? AuthorId { get; set; }

        [Display(Name = "Nome do Autor")]
        public string? Author { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }

        [Display(Name = "Categorias")]
        public string? Category { get; set; }
        public string? Views { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal ViewsCount { get; set; }
        public int Likes { get; set; }
        public DateTime PubDate { get; set; }
    }
}
