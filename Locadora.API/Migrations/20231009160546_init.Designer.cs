﻿// <auto-generated />
using Locadora.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Locadora.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231009160546_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("Locadora.API.Models.Books", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Release")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rented")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "J.R.R. Tolkien",
                            Name = "O Senhor dos Anéis",
                            PublisherId = 1,
                            Quantity = 10,
                            Release = "1954",
                            Rented = 0
                        },
                        new
                        {
                            Id = 2,
                            Author = "J.K. Rowling",
                            Name = "Harry Potter e a Pedra Filosofal",
                            PublisherId = 2,
                            Quantity = 1,
                            Release = "1997",
                            Rented = 0
                        },
                        new
                        {
                            Id = 3,
                            Author = "Miguel de Cervantes",
                            Name = "Dom Quixote",
                            PublisherId = 3,
                            Quantity = 1,
                            Release = "1605",
                            Rented = 0
                        },
                        new
                        {
                            Id = 4,
                            Author = "Gabriel García Márquez",
                            Name = "Cem Anos de Solidão",
                            PublisherId = 4,
                            Quantity = 12,
                            Release = "1967",
                            Rented = 0
                        },
                        new
                        {
                            Id = 5,
                            Author = "George Orwell",
                            Name = "1984",
                            PublisherId = 5,
                            Quantity = 7,
                            Release = "1949",
                            Rented = 0
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Publishers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "São Paulo",
                            Name = "Editora Nacional"
                        },
                        new
                        {
                            Id = 2,
                            City = "Rio de Janeiro",
                            Name = "Editora Regional"
                        },
                        new
                        {
                            Id = 3,
                            City = "Belo Horizonte",
                            Name = "Editora Local"
                        },
                        new
                        {
                            Id = 4,
                            City = "Brasília",
                            Name = "Editora Central"
                        },
                        new
                        {
                            Id = 5,
                            City = "Porto Alegre",
                            Name = "Editora do Sul"
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Rentals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForecastDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RentalDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Locadora.API.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Rua A",
                            City = "Fortaleza",
                            Email = "lauro@yahoo.com",
                            Name = "Lauro"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Rua B",
                            City = "Crato",
                            Email = "roberto@gmail.com",
                            Name = "Roberto"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Rua C",
                            City = "Caucaia",
                            Email = "ronaldo@hotmail.com",
                            Name = "Ronaldo"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Rua D",
                            City = "Recife",
                            Email = "rodrigo@gmail.com",
                            Name = "Rodrigo"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Rua E",
                            City = "Rio de Janeiro",
                            Email = "alexandre@yahoo.com",
                            Name = "Alexandre"
                        });
                });

            modelBuilder.Entity("Locadora.API.Models.Books", b =>
                {
                    b.HasOne("Locadora.API.Models.Publishers", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Locadora.API.Models.Rentals", b =>
                {
                    b.HasOne("Locadora.API.Models.Books", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Locadora.API.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}