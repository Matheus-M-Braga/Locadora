using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false)
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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: false),
                    Release = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Rented = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ForecastDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
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
                    { 1, "São Paulo", "Companhia das Letras" },
                    { 2, "Rio de Janeiro", "Aleph" },
                    { 3, "Rio De Janeiro", "Editora Intrínseca" },
                    { 4, "Rio de Janeiro", "Editora Rocco" },
                    { 5, "Porto Alegre", "Darkside" },
                    { 6, "Nova Iorque", "Harper Collins" },
                    { 7, "Rio de Janeiro", "Editora Arqueiro" },
                    { 8, "Lisboa", "Leya" },
                    { 9, "São Paulo", "Saraiva" },
                    { 10, "Porto Alegre", "Sextante" }
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
                    { 5, "Rua E", "Rio de Janeiro", "alexandre@yahoo.com", "Alexandre" },
                    { 6, "Rua F", "Fortaleza", "isabela@gmail.com", "Isabela" },
                    { 7, "Rua G", "São Paulo", "pedro@hotmail.com", "Pedro" },
                    { 8, "Rua H", "Rio de Janeiro", "mariana@yahoo.com", "Mariana" },
                    { 9, "Rua I", "Belo Horizonte", "lucas@gmail.com", "Lucas" },
                    { 10, "Rua J", "Recife", "amanda@hotmail.com", "Amanda" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name", "PublisherId", "Quantity", "Release", "Rented" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", "O Senhor dos Anéis", 1, 10, 1954, 0 },
                    { 2, "J.K. Rowling", "Harry Potter e a Pedra Filosofal", 2, 1, 1997, 0 },
                    { 3, "Miguel de Cervantes", "Dom Quixote", 3, 1, 1605, 0 },
                    { 4, "Gabriel García Márquez", "Cem Anos de Solidão", 4, 12, 1954, 0 },
                    { 5, "George Orwell", "1984", 5, 7, 1954, 0 },
                    { 6, "George Orwell", "A Revolução dos Bichos", 1, 5, 1954, 0 },
                    { 7, "Fiódor Dostoiévski", "Crime e Castigo", 2, 3, 1954, 0 },
                    { 8, "Antoine de Saint-Exupéry", "O Pequeno Príncipe", 3, 8, 1954, 0 },
                    { 9, "Machado de Assis", "Memórias Póstumas de Brás Cubas", 4, 6, 1954, 0 },
                    { 10, "Franz Kafka", "A Metamorfose", 5, 4, 1954, 0 }
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
