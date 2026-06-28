using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetStreaming.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPlaylistMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musica_Playlist_PlaylistId",
                table: "Musica");

            migrationBuilder.DropIndex(
                name: "IX_Musica_PlaylistId",
                table: "Musica");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Musica");

            migrationBuilder.CreateTable(
                name: "PlaylistMusica",
                columns: table => new
                {
                    MusicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistMusica", x => new { x.MusicaId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_PlaylistMusica_Musica_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "Musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistMusica_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusica_PlaylistId",
                table: "PlaylistMusica",
                column: "PlaylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistMusica");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistId",
                table: "Musica",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musica_PlaylistId",
                table: "Musica",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musica_Playlist_PlaylistId",
                table: "Musica",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id");
        }
    }
}
