namespace BibliotecaDigital.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
    }
}