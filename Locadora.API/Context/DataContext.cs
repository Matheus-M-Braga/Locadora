﻿#pragma warning disable CS8618
using Locadora.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Context
{

    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Locadora.db");
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
                .HasData(new List<Users>(){
                    new Users(1, "Lauro", "Fortaleza", "Rua A", "lauro@yahoo.com"),
                    new Users(2, "Roberto", "Crato", "Rua B", "roberto@gmail.com"),
                    new Users(3, "Ronaldo", "Caucaia", "Rua C", "ronaldo@hotmail.com"),
                    new Users(4, "Rodrigo", "Recife", "Rua D", "rodrigo@gmail.com"),
                    new Users(5, "Alexandre", "Rio de Janeiro", "Rua E", "alexandre@yahoo.com"),
                });

            builder.Entity<Books>()
                .HasData(new List<Books>{
                    new Books(1, "O Senhor dos Anéis", "J.R.R. Tolkien", 1, "1954", 10, 0),
                    new Books(2, "Harry Potter e a Pedra Filosofal", "J.K. Rowling", 2, "1997", 1, 0),
                    new Books(3, "Dom Quixote", "Miguel de Cervantes", 3, "1605", 1, 0),
                    new Books(4, "Cem Anos de Solidão", "Gabriel García Márquez", 4, "1967", 12, 0),
                    new Books(5, "1984", "George Orwell", 5, "1949", 7, 0),
                });

            builder.Entity<Publishers>()
                .HasData(new List<Publishers>{
                    new Publishers(1, "Editora Nacional", "São Paulo"),
                    new Publishers(2, "Editora Regional", "Rio de Janeiro"),
                    new Publishers(3, "Editora Local", "Belo Horizonte"),
                    new Publishers(4, "Editora Central", "Brasília"),
                    new Publishers(5, "Editora do Sul", "Porto Alegre")
                });
        }
    }
}