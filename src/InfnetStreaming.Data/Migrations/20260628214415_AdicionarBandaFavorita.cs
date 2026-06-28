using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetStreaming.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarBandaFavorita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Musica",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Banda",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BandaFavorita",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataFavoritado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandaFavorita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BandaFavorita_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musica_Nome",
                table: "Musica",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Banda_Nome",
                table: "Banda",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_BandaFavorita_UsuarioId",
                table: "BandaFavorita",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BandaFavorita");

            migrationBuilder.DropIndex(
                name: "IX_Musica_Nome",
                table: "Musica");

            migrationBuilder.DropIndex(
                name: "IX_Banda_Nome",
                table: "Banda");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Musica",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Banda",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
