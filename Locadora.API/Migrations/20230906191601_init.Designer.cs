﻿// <auto-generated />
using System;
using Locadora.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Locadora.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230906191601_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Locadora.API.Models.Aluguel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataAluguel")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataPrevisao")
                        .HasColumnType("TEXT");

                    b.Property<int>("LivroId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Alugueis");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataAluguel = new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataPrevisao = new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LivroId = 1,
                            Status = "Pendente",
                            UsuarioId = 1
                        },
                        new
                        {
                            Id = 2,
                            DataAluguel = new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataPrevisao = new DateTime(2023, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LivroId = 2,
                            Status = "No Prazo",
                            UsuarioId = 2
                        },
                        new
                        {
                            Id = 3,
                            DataAluguel = new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataPrevisao = new DateTime(2023, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LivroId = 3,
                            Status = "Atrasado",
                            UsuarioId = 3
                        },
                        new
                        {
                            Id = 4,
                            DataAluguel = new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataPrevisao = new DateTime(2023, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LivroId = 4,
                            Status = "Pendente",
                            UsuarioId = 4
                        },
                        new
                        {
                            Id = 5,
                            DataAluguel = new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataPrevisao = new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LivroId = 5,
                            Status = "No Prazo",
                            UsuarioId = 5
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Editora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Editoras");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cidade = "São Paulo",
                            Nome = "Editora Nacional"
                        },
                        new
                        {
                            Id = 2,
                            Cidade = "Rio de Janeiro",
                            Nome = "Editora Regional"
                        },
                        new
                        {
                            Id = 3,
                            Cidade = "Belo Horizonte",
                            Nome = "Editora Local"
                        },
                        new
                        {
                            Id = 4,
                            Cidade = "Brasília",
                            Nome = "Editora Central"
                        },
                        new
                        {
                            Id = 5,
                            Cidade = "Porto Alegre",
                            Nome = "Editora do Sul"
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Alugados")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EditoraId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lancamento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quatidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EditoraId");

                    b.ToTable("Livros");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alugados = 0,
                            Autor = "J.R.R. Tolkien",
                            EditoraId = 1,
                            Lancamento = "1954",
                            Nome = "O Senhor dos Anéis",
                            Quatidade = 10
                        },
                        new
                        {
                            Id = 2,
                            Alugados = 0,
                            Autor = "J.K. Rowling",
                            EditoraId = 2,
                            Lancamento = "1997",
                            Nome = "Harry Potter e a Pedra Filosofal",
                            Quatidade = 15
                        },
                        new
                        {
                            Id = 3,
                            Alugados = 0,
                            Autor = "Miguel de Cervantes",
                            EditoraId = 3,
                            Lancamento = "1605",
                            Nome = "Dom Quixote",
                            Quatidade = 8
                        },
                        new
                        {
                            Id = 4,
                            Alugados = 0,
                            Autor = "Gabriel García Márquez",
                            EditoraId = 4,
                            Lancamento = "1967",
                            Nome = "Cem Anos de Solidão",
                            Quatidade = 12
                        },
                        new
                        {
                            Id = 5,
                            Alugados = 0,
                            Autor = "George Orwell",
                            EditoraId = 5,
                            Lancamento = "1949",
                            Nome = "1984",
                            Quatidade = 7
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cidade = "Fortaleza",
                            Email = "lauro@yahoo.com",
                            Endereco = "Rua A",
                            Nome = "Lauro"
                        },
                        new
                        {
                            Id = 2,
                            Cidade = "Crato",
                            Email = "roberto@gmail.com",
                            Endereco = "Rua B",
                            Nome = "Roberto"
                        },
                        new
                        {
                            Id = 3,
                            Cidade = "Caucaia",
                            Email = "ronaldo@hotmail.com",
                            Endereco = "Rua C",
                            Nome = "Ronaldo"
                        },
                        new
                        {
                            Id = 4,
                            Cidade = "Recife",
                            Email = "rodrigo@gmail.com",
                            Endereco = "Rua D",
                            Nome = "Rodrigo"
                        },
                        new
                        {
                            Id = 5,
                            Cidade = "Rio de Janeiro",
                            Email = "alexandre@yahoo.com",
                            Endereco = "Rua E",
                            Nome = "Alexandre"
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Aluguel", b =>
                {
                    b.HasOne("Locadora.API.Models.Livro", "Livro")
                        .WithMany("Aluguel")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Locadora.API.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Locadora.API.Models.Livro", b =>
                {
                    b.HasOne("Locadora.API.Models.Editora", "Editora")
                        .WithMany("Livros")
                        .HasForeignKey("EditoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editora");
                });

            modelBuilder.Entity("Locadora.API.Models.Editora", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("Locadora.API.Models.Livro", b =>
                {
                    b.Navigation("Aluguel");
                });
#pragma warning restore 612, 618
        }
    }
}
