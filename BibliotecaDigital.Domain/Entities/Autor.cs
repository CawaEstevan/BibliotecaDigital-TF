namespace BibliotecaDigital.Domain.Entities
{
    public class Autor : EntityBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nacionalidade { get; set; } = string.Empty;
        public string Biografia { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        
       
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}