using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locadora.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Editoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Autor = table.Column<string>(type: "TEXT", nullable: false),
                    EditoraId = table.Column<int>(type: "INTEGER", nullable: false),
                    Lancamento = table.Column<string>(type: "TEXT", nullable: false),
                    Quatidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Alugados = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livros_Editoras_EditoraId",
                        column: x => x.EditoraId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LivroId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataAluguel = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPrevisao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataDevolucao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alugueis_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alugueis_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Cidade", "Nome" },
                values: new object[] { 1, "São Paulo", "Editora Nacional" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Cidade", "Nome" },
                values: new object[] { 2, "Rio de Janeiro", "Editora Regional" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Cidade", "Nome" },
                values: new object[] { 3, "Belo Horizonte", "Editora Local" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Cidade", "Nome" },
                values: new object[] { 4, "Brasília", "Editora Central" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Cidade", "Nome" },
                values: new object[] { 5, "Porto Alegre", "Editora do Sul" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cidade", "Email", "Endereco", "Nome" },
                values: new object[] { 1, "Fortaleza", "lauro@yahoo.com", "Rua A", "Lauro" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cidade", "Email", "Endereco", "Nome" },
                values: new object[] { 2, "Crato", "roberto@gmail.com", "Rua B", "Roberto" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cidade", "Email", "Endereco", "Nome" },
                values: new object[] { 3, "Caucaia", "ronaldo@hotmail.com", "Rua C", "Ronaldo" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cidade", "Email", "Endereco", "Nome" },
                values: new object[] { 4, "Recife", "rodrigo@gmail.com", "Rua D", "Rodrigo" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cidade", "Email", "Endereco", "Nome" },
                values: new object[] { 5, "Rio de Janeiro", "alexandre@yahoo.com", "Rua E", "Alexandre" });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Alugados", "Autor", "EditoraId", "Lancamento", "Nome", "Quatidade" },
                values: new object[] { 1, 0, "J.R.R. Tolkien", 1, "1954", "O Senhor dos Anéis", 10 });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Alugados", "Autor", "EditoraId", "Lancamento", "Nome", "Quatidade" },
                values: new object[] { 2, 0, "J.K. Rowling", 2, "1997", "Harry Potter e a Pedra Filosofal", 15 });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Alugados", "Autor", "EditoraId", "Lancamento", "Nome", "Quatidade" },
                values: new object[] { 3, 0, "Miguel de Cervantes", 3, "1605", "Dom Quixote", 8 });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Alugados", "Autor", "EditoraId", "Lancamento", "Nome", "Quatidade" },
                values: new object[] { 4, 0, "Gabriel García Márquez", 4, "1967", "Cem Anos de Solidão", 12 });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Alugados", "Autor", "EditoraId", "Lancamento", "Nome", "Quatidade" },
                values: new object[] { 5, 0, "George Orwell", 5, "1949", "1984", 7 });

            migrationBuilder.InsertData(
                table: "Alugueis",
                columns: new[] { "Id", "DataAluguel", "DataDevolucao", "DataPrevisao", "LivroId", "Status", "UsuarioId" },
                values: new object[] { 1, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pendente", 1 });

            migrationBuilder.InsertData(
                table: "Alugueis",
                columns: new[] { "Id", "DataAluguel", "DataDevolucao", "DataPrevisao", "LivroId", "Status", "UsuarioId" },
                values: new object[] { 2, new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "No Prazo", 2 });

            migrationBuilder.InsertData(
                table: "Alugueis",
                columns: new[] { "Id", "DataAluguel", "DataDevolucao", "DataPrevisao", "LivroId", "Status", "UsuarioId" },
                values: new object[] { 3, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Atrasado", 3 });

            migrationBuilder.InsertData(
                table: "Alugueis",
                columns: new[] { "Id", "DataAluguel", "DataDevolucao", "DataPrevisao", "LivroId", "Status", "UsuarioId" },
                values: new object[] { 4, new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Pendente", 4 });

            migrationBuilder.InsertData(
                table: "Alugueis",
                columns: new[] { "Id", "DataAluguel", "DataDevolucao", "DataPrevisao", "LivroId", "Status", "UsuarioId" },
                values: new object[] { 5, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "No Prazo", 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_LivroId",
                table: "Alugueis",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_UsuarioId",
                table: "Alugueis",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EditoraId",
                table: "Livros",
                column: "EditoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Editoras");
        }
    }
}
