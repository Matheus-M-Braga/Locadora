#pragma warning disable CS8618
using Locadora.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Locadora.API.Context {
    public class DataContext : DbContext {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Rentals> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite("Data Source=Locadora.db");
            }
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Users>()
                .HasData(new List<Users>{
                    new Users(1, "Lauro", "Fortaleza", "Rua A", "lauro@yahoo.com"),
                    new Users(2, "Roberto", "Crato", "Rua B", "roberto@gmail.com"),
                    new Users(3, "Ronaldo", "Caucaia", "Rua C", "ronaldo@hotmail.com"),
                    new Users(4, "Rodrigo", "Recife", "Rua D", "rodrigo@gmail.com"),
                    new Users(5, "Alexandre", "Rio de Janeiro", "Rua E", "alexandre@yahoo.com"),
                    new Users(6, "Isabela", "Fortaleza", "Rua F", "isabela@gmail.com"),
                    new Users(7, "Pedro", "São Paulo", "Rua G", "pedro@hotmail.com"),
                    new Users(8, "Mariana", "Rio de Janeiro", "Rua H", "mariana@yahoo.com"),
                    new Users(9, "Lucas", "Belo Horizonte", "Rua I", "lucas@gmail.com"),
                    new Users(10, "Amanda", "Recife", "Rua J", "amanda@hotmail.com"),
                });

            builder.Entity<Books>()
                .HasData(new List<Books>{
                    new Books(1, "O Senhor dos Anéis", "J.R.R. Tolkien", 1, "1954", 10, 0),
                    new Books(2, "Harry Potter e a Pedra Filosofal", "J.K. Rowling", 2, "1997", 1, 0),
                    new Books(3, "Dom Quixote", "Miguel de Cervantes", 3, "1605", 1, 0),
                    new Books(4, "Cem Anos de Solidão", "Gabriel García Márquez", 4, "1967", 12, 0),
                    new Books(5, "1984", "George Orwell", 5, "1949", 7, 0),
                    new Books(6, "A Revolução dos Bichos", "George Orwell", 1, "1945", 5, 0),
                    new Books(7, "Crime e Castigo", "Fiódor Dostoiévski", 2, "1866", 3, 0),
                    new Books(8, "O Pequeno Príncipe", "Antoine de Saint-Exupéry", 3, "1943", 8, 0),
                    new Books(9, "Memórias Póstumas de Brás Cubas", "Machado de Assis", 4, "1881", 6, 0),
                    new Books(10, "A Metamorfose", "Franz Kafka", 5, "1915", 4, 0),
                });

            builder.Entity<Publishers>()
                .HasData(new List<Publishers>{
                    new Publishers(1, "Editora Nacional", "São Paulo"),
                    new Publishers(2, "Editora Regional", "Rio de Janeiro"),
                    new Publishers(3, "Editora Local", "Belo Horizonte"),
                    new Publishers(4, "Editora Central", "Brasília"),
                    new Publishers(5, "Editora do Sul", "Porto Alegre"),
                    new Publishers(6, "Editora Nacional 2", "São Paulo"),
                    new Publishers(7, "Editora Regional 2", "Rio de Janeiro"),
                    new Publishers(8, "Editora Local 2", "Belo Horizonte"),
                    new Publishers(9, "Editora Central 2", "Brasília"),
                    new Publishers(10, "Editora do Sul 2", "Porto Alegre"),
                });
        }
    }
}
