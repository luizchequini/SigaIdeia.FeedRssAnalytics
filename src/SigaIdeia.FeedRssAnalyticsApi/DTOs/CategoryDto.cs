using System.ComponentModel.DataAnnotations;

namespace SigaIdeia.FeedRssAnalyticsApi.DTOs
{
    public class CategoryDto
    {
        [Display(Name = "Nome da categoria")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Required(ErrorMessage = "O campo {0} é requirido")]
        public string? Name { get; set; }

        [Display(Name = "Número de Posts nesta categoria")]
        [Required(ErrorMessage = "O campo {0} é requirido")]
        public int Count { get; set; }
    }
}
