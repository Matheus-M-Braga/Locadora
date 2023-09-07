using Locadora.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Data {

    public class DataContext : DbContext {
        public DataContext() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite("Data Source=Locadora.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Usuario>()
                .HasData(new List<Usuario>(){
                    new Usuario(1, "Lauro", "Fortaleza", "Rua A", "lauro@yahoo.com"),
                    new Usuario(2, "Roberto", "Crato", "Rua B", "roberto@gmail.com"),
                    new Usuario(3, "Ronaldo", "Caucaia", "Rua C", "ronaldo@hotmail.com"),
                    new Usuario(4, "Rodrigo", "Recife", "Rua D", "rodrigo@gmail.com"),
                    new Usuario(5, "Alexandre", "Rio de Janeiro", "Rua E", "alexandre@yahoo.com"),
                });

            builder.Entity<Livro>()
                .HasData(new List<Livro>{
                    new Livro(1, "O Senhor dos Anéis", "J.R.R. Tolkien", 1, "1954", 10, 0),
                    new Livro(2, "Harry Potter e a Pedra Filosofal", "J.K. Rowling", 2, "1997", 15, 0),
                    new Livro(3, "Dom Quixote", "Miguel de Cervantes", 3, "1605", 8, 0),
                    new Livro(4, "Cem Anos de Solidão", "Gabriel García Márquez", 4, "1967", 12, 0),
                    new Livro(5, "1984", "George Orwell", 5, "1949", 7, 0),
                });

            builder.Entity<Editora>()
                .HasData(new List<Editora>{
                    new Editora(1, "Editora Nacional", "São Paulo"),
                    new Editora(2, "Editora Regional", "Rio de Janeiro"),
                    new Editora(3, "Editora Local", "Belo Horizonte"),
                    new Editora(4, "Editora Central", "Brasília"),
                    new Editora(5, "Editora do Sul", "Porto Alegre")
                });

            builder.Entity<Aluguel>()
                .HasData(new List<Aluguel>{
                    new Aluguel(1, 1, 1, new DateTime(2023, 9, 10), new DateTime(2023, 9, 20), null, "Pendente"),
                    new Aluguel(2, 2, 2, new DateTime(2023, 9, 12), new DateTime(2023, 9, 22), null, "No Prazo"),
                    new Aluguel(3, 3, 3, new DateTime(2023, 9, 15), new DateTime(2023, 9, 25), null, "Atrasado"),
                    new Aluguel(4, 4, 4, new DateTime(2023, 9, 18), new DateTime(2023, 9, 28), null, "Pendente"),
                    new Aluguel(5, 5, 5, new DateTime(2023, 9, 20), new DateTime(2023, 9, 30), null, "No Prazo")
                });

        }
    }
}
