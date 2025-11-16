using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Application.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "ISBN deve estar no formato XXX-XXXXXXXXXX")]
        public string ISBN { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "A editora não pode exceder 100 caracteres")]
        [Display(Name = "Editora")]
        public string Editora { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1500, 2025, ErrorMessage = "Ano deve estar entre 1500 e 2025")]
        [Display(Name = "Ano de Publicação")]
        public int AnoPublicacao { get; set; }
        
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 10000, ErrorMessage = "Preço deve estar entre R$ 0,01 e R$ 10.000,00")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "O número de páginas é obrigatório")]
        [Range(1, 10000, ErrorMessage = "Número de páginas deve estar entre 1 e 10.000")]
        [Display(Name = "Número de Páginas")]
        public int NumeroPaginas { get; set; }
        
        [Required(ErrorMessage = "Selecione um autor")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }
        
        // Propriedades de navegação/exibição
        public string? NomeAutor { get; set; }
        public AutorViewModel? Autor { get; set; }
    }
}