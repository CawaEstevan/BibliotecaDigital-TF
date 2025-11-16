using System.ComponentModel.DataAnnotations;
using BibliotecaDigital.Application.Validations;

namespace BibliotecaDigital.Application.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [EmailUnico] // Validação personalizada 1
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A nacionalidade é obrigatória")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A nacionalidade deve ter entre 3 e 50 caracteres")]
        public string Nacionalidade { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "A biografia não pode exceder 500 caracteres")]
        public string Biografia { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataNascimentoValida] // Validação personalizada 2
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        
        // Lista de livros do autor (para exibição)
        public ICollection<LivroViewModel> Livros { get; set; } = new List<LivroViewModel>();
    }
}