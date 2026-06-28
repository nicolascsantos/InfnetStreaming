using InfnetStreaming.Application.Services.Usuario.FavoritarBanda;
using InfnetStreaming.Application.Services.Usuario.FavoritarMusica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("usuarios/{usuarioId:guid}/favoritos")]
    [Authorize]
    public class FavoritosController : ControllerBase
    {
        [HttpPost("musicas/{musicaId:guid}")]
        public async Task<IActionResult> FavoritarMusica(
            Guid usuarioId,
            Guid musicaId,
            [FromServices] FavoritarMusicaService service,
            CancellationToken cancellationToken)
        {
            await service.Executar(new FavoritarMusicaInput(usuarioId, musicaId, Favoritar: true), cancellationToken);
            return NoContent();
        }

        [HttpDelete("musicas/{musicaId:guid}")]
        public async Task<IActionResult> DesfavoritarMusica(
            Guid usuarioId,
            Guid musicaId,
            [FromServices] FavoritarMusicaService service,
            CancellationToken cancellationToken)
        {
            await service.Executar(new FavoritarMusicaInput(usuarioId, musicaId, Favoritar: false), cancellationToken);
            return NoContent();
        }

        [HttpPost("bandas/{bandaId:guid}")]
        public async Task<IActionResult> FavoritarBanda(
            Guid usuarioId,
            Guid bandaId,
            [FromServices] FavoritarBandaService service,
            CancellationToken cancellationToken)
        {
            await service.Executar(new FavoritarBandaInput(usuarioId, bandaId, Favoritar: true), cancellationToken);
            return NoContent();
        }

        [HttpDelete("bandas/{bandaId:guid}")]
        public async Task<IActionResult> DesfavoritarBanda(
            Guid usuarioId,
            Guid bandaId,
            [FromServices] FavoritarBandaService service,
            CancellationToken cancellationToken)
        {
            await service.Executar(new FavoritarBandaInput(usuarioId, bandaId, Favoritar: false), cancellationToken);
            return NoContent();
        }
    }
}
