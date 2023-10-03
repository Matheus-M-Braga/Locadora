﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Locadora.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: true),
                    Release = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    Rented = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentalDate = table.Column<string>(type: "TEXT", nullable: true),
                    ForecastDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReturnDate = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "City", "Name" },
                values: new object[,]
                {
                    { 1, "São Paulo", "Editora Nacional" },
                    { 2, "Rio de Janeiro", "Editora Regional" },
                    { 3, "Belo Horizonte", "Editora Local" },
                    { 4, "Brasília", "Editora Central" },
                    { 5, "Porto Alegre", "Editora do Sul" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "City", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "Rua A", "Fortaleza", "lauro@yahoo.com", "Lauro" },
                    { 2, "Rua B", "Crato", "roberto@gmail.com", "Roberto" },
                    { 3, "Rua C", "Caucaia", "ronaldo@hotmail.com", "Ronaldo" },
                    { 4, "Rua D", "Recife", "rodrigo@gmail.com", "Rodrigo" },
                    { 5, "Rua E", "Rio de Janeiro", "alexandre@yahoo.com", "Alexandre" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name", "PublisherId", "Quantity", "Release", "Rented" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", "O Senhor dos Anéis", 1, 10, "1954", 0 },
                    { 2, "J.K. Rowling", "Harry Potter e a Pedra Filosofal", 2, 1, "1997", 0 },
                    { 3, "Miguel de Cervantes", "Dom Quixote", 3, 1, "1605", 0 },
                    { 4, "Gabriel García Márquez", "Cem Anos de Solidão", 4, 12, "1967", 0 },
                    { 5, "George Orwell", "1984", 5, 7, "1949", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookId",
                table: "Rentals",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}