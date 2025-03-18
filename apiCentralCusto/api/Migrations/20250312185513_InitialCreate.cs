using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaEntradas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaSaidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaSaidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Credencial = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CentralCustoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentralCustos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralCustos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralCustos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LancamentoEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaEntradaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DuracaoMeses = table.Column<int>(type: "INTEGER", nullable: false),
                    PagamentoConfirmado = table.Column<bool>(type: "INTEGER", nullable: false),
                    EstadoLancamento = table.Column<int>(type: "INTEGER", nullable: false),
                    CentralCustoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoEntradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LancamentoEntradas_CategoriaEntradas_CategoriaEntradaId",
                        column: x => x.CategoriaEntradaId,
                        principalTable: "CategoriaEntradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LancamentoEntradas_CentralCustos_CentralCustoId",
                        column: x => x.CentralCustoId,
                        principalTable: "CentralCustos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LancamentoSaidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaSaidaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PagamentoConfirmado = table.Column<bool>(type: "INTEGER", nullable: false),
                    EstadoLancamento = table.Column<int>(type: "INTEGER", nullable: false),
                    CentralCustoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LancamentoSaidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LancamentoSaidas_CategoriaSaidas_CategoriaSaidaId",
                        column: x => x.CategoriaSaidaId,
                        principalTable: "CategoriaSaidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LancamentoSaidas_CentralCustos_CentralCustoId",
                        column: x => x.CentralCustoId,
                        principalTable: "CentralCustos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralCustos_UsuarioId",
                table: "CentralCustos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoEntradas_CategoriaEntradaId",
                table: "LancamentoEntradas",
                column: "CategoriaEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoEntradas_CentralCustoId",
                table: "LancamentoEntradas",
                column: "CentralCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoSaidas_CategoriaSaidaId",
                table: "LancamentoSaidas",
                column: "CategoriaSaidaId");

            migrationBuilder.CreateIndex(
                name: "IX_LancamentoSaidas_CentralCustoId",
                table: "LancamentoSaidas",
                column: "CentralCustoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LancamentoEntradas");

            migrationBuilder.DropTable(
                name: "LancamentoSaidas");

            migrationBuilder.DropTable(
                name: "CategoriaEntradas");

            migrationBuilder.DropTable(
                name: "CategoriaSaidas");

            migrationBuilder.DropTable(
                name: "CentralCustos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
