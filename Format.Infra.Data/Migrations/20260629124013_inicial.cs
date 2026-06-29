using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Format.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.IdTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    IdDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_Documento_TipoDocumento_IdTipoDocumento",
                        column: x => x.IdTipoDocumento,
                        principalTable: "TipoDocumento",
                        principalColumn: "IdTipoDocumento",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Documento_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Referencias",
                columns: table => new
                {
                    IdReferencias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdDocumento = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referencias", x => x.IdReferencias);
                    table.ForeignKey(
                        name: "FK_Referencias_Documento_IdDocumento",
                        column: x => x.IdDocumento,
                        principalTable: "Documento",
                        principalColumn: "IdDocumento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Secoes",
                columns: table => new
                {
                    IdSecoes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDocumento = table.Column<int>(type: "int", nullable: false),
                    IdPai = table.Column<int>(type: "int", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secoes", x => x.IdSecoes);
                    table.ForeignKey(
                        name: "FK_Secoes_Documento_IdDocumento",
                        column: x => x.IdDocumento,
                        principalTable: "Documento",
                        principalColumn: "IdDocumento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Secoes_Secoes_IdPai",
                        column: x => x.IdPai,
                        principalTable: "Secoes",
                        principalColumn: "IdSecoes",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecoesConteudo",
                columns: table => new
                {
                    IdSecoesConteudo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSecoes = table.Column<int>(type: "int", nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecoesConteudo", x => x.IdSecoesConteudo);
                    table.ForeignKey(
                        name: "FK_SecoesConteudo_Secoes_IdSecoes",
                        column: x => x.IdSecoes,
                        principalTable: "Secoes",
                        principalColumn: "IdSecoes",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documento_IdTipoDocumento",
                table: "Documento",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_IdUsuario",
                table: "Documento",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Referencias_IdDocumento",
                table: "Referencias",
                column: "IdDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Secoes_IdDocumento",
                table: "Secoes",
                column: "IdDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Secoes_IdPai",
                table: "Secoes",
                column: "IdPai");

            migrationBuilder.CreateIndex(
                name: "IX_SecoesConteudo_IdSecoes",
                table: "SecoesConteudo",
                column: "IdSecoes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Referencias");

            migrationBuilder.DropTable(
                name: "SecoesConteudo");

            migrationBuilder.DropTable(
                name: "Secoes");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
