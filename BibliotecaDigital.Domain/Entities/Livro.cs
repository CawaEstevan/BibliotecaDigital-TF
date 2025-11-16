namespace BibliotecaDigital.Domain.Entities
{
    public class Livro : EntityBase
    {
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public decimal Preco { get; set; }
        public int NumeroPaginas { get; set; }
        
        // Chave estrangeira explícita (OBRIGATÓRIO pelo PDF)
        public int AutorId { get; set; }
        
        // Propriedade de navegação
        public Autor Autor { get; set; } = null!;
    }
}