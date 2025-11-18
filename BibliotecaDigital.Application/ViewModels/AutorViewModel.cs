using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BibliotecaDigital.Application.Validations;

namespace BibliotecaDigital.Application.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailUnico] 
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A nacionalidade é obrigatória")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "A nacionalidade deve ter entre 3 e 50 caracteres")]
        [Display(Name = "Nacionalidade")]
        public string Nacionalidade { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "A biografia não pode exceder 500 caracteres")]
        [Display(Name = "Biografia")]
        public string Biografia { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataNascimentoValida] 
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        
  
        public ICollection<LivroViewModel> Livros { get; set; } = new List<LivroViewModel>();
    }
}