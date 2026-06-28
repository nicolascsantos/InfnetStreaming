using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetStreaming.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFormacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Album_Banda_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Banda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genero_Banda_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Banda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Integrante",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDeNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrante_Banda_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Banda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicaFavorita",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataFavoritado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaFavorita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicaFavorita_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlist_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Musica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    OrdemMusica = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musica_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Musica_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicaBanda",
                columns: table => new
                {
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaBanda", x => new { x.BandaId, x.MusicaId });
                    table.ForeignKey(
                        name: "FK_MusicaBanda_Banda_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Banda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaBanda_Musica_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "Musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_BandaId",
                table: "Album",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_BandaId",
                table: "Genero",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Integrante_BandaId",
                table: "Integrante",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Musica_AlbumId",
                table: "Musica",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Musica_PlaylistId",
                table: "Musica",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaBanda_MusicaId",
                table: "MusicaBanda",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaFavorita_UsuarioId",
                table: "MusicaFavorita",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UsuarioId",
                table: "Playlist",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PlanoId",
                table: "Usuario",
                column: "PlanoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Integrante");

            migrationBuilder.DropTable(
                name: "MusicaBanda");

            migrationBuilder.DropTable(
                name: "MusicaFavorita");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "Musica");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Playlist");

            migrationBuilder.DropTable(
                name: "Banda");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Plano");
        }
    }
}
