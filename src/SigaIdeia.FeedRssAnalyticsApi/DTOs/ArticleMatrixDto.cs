using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigaIdeia.FeedRssAnalyticsApi.DTOs
{
    public class ArticleMatrixDto
    {
        [Key]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Autor Id")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string? AuthorId { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string? Author { get; set; }

        [Display(Name = "Link")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Link { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string? Title { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string? Type { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string? Category { get; set; }
        public string? Views { get; set; }

        [Column(TypeName = "decimal(18,4")]
        public decimal ViewsCount { get; set; }

        public int Likes { get; set; }

        [Display(Name = "Data da Publicação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime PubDate { get; set; }
    }
}
