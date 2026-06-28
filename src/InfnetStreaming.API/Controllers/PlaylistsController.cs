using InfnetStreaming.Application.Services.Playlist.AdicionarMusicaPlaylist;
using InfnetStreaming.Application.Services.Playlist.CriarPlaylist;
using InfnetStreaming.Application.Services.Playlist.ListarPlaylists;
using InfnetStreaming.Application.Services.Playlist.ObterPlaylist;
using InfnetStreaming.Application.Services.Playlist.RemoverMusicaPlaylist;
using InfnetStreaming.Domain.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("usuarios/{usuarioId:guid}/playlists")]
    [Authorize]
    public class PlaylistsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Listar(
            Guid usuarioId,
            [FromServices] ListarPlaylistsService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(usuarioId, cancellationToken);
            return Ok(output);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(
            Guid usuarioId,
            [FromBody] CriarPlaylistRequest request,
            [FromServices] CriarPlaylistService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(new CriarPlaylistInput(usuarioId, request.Nome), cancellationToken);
            return Created($"usuarios/{usuarioId}/playlists/{output.Id}", output);
        }

        [HttpGet("{playlistId:guid}")]
        public async Task<IActionResult> Obter(
            Guid playlistId,
            [FromServices] ObterPlaylistService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(playlistId, cancellationToken);
            return Ok(output);
        }

        [HttpDelete("{playlistId:guid}")]
        public async Task<IActionResult> Deletar(
            Guid playlistId,
            [FromServices] IRepositorioPlaylist repositorio,
            CancellationToken cancellationToken)
        {
            await repositorio.Deletar(playlistId, cancellationToken);
            return NoContent();
        }

        [HttpPost("{playlistId:guid}/musicas/{musicaId:guid}")]
        public async Task<IActionResult> AdicionarMusica(
            Guid playlistId,
            Guid musicaId,
            [FromServices] AdicionarMusicaPlaylistService service,
            CancellationToken cancellationToken)
        {
            await service.Executar(new AdicionarMusicaPlaylistInput(playlistId, musicaId), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{playlistId:guid}/musicas/{musicaId:guid}")]
        public async Task<IActionResult> RemoverMusica(
            Guid playlistId,
            Guid musicaId,
            [FromServices] RemoverMusicaPlaylistService service,
            CancellationToken cancellationToken)
        {
            await service.Executar(new RemoverMusicaPlaylistInput(playlistId, musicaId), cancellationToken);
            return NoContent();
        }
    }

    public record CriarPlaylistRequest(string Nome);
}
