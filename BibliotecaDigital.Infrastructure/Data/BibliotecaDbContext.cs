using BibliotecaDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Autor
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("Autores");
                entity.HasKey(a => a.Id);
                
                entity.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(a => a.Email)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(a => a.Biografia)
                    .HasMaxLength(1000);

                entity.Property(a => a.DataNascimento)
                    .IsRequired();

                // Relacionamento 1:N
                entity.HasMany(a => a.Livros)
                    .WithOne(l => l.Autor)
                    .HasForeignKey(l => l.AutorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da entidade Livro
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable("Livros");
                entity.HasKey(l => l.Id);
                
                entity.Property(l => l.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(l => l.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasIndex(l => l.ISBN).IsUnique();
                
                entity.Property(l => l.AnoPublicacao)
                    .IsRequired();

                entity.Property(l => l.Preco)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");

                entity.Property(l => l.NumeroPaginas)
                    .IsRequired();

                // Chave estrangeira explícita
                entity.Property(l => l.AutorId)
                    .IsRequired();
            });
        }
    }
}