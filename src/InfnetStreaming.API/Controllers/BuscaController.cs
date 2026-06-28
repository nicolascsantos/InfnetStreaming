using InfnetStreaming.Application.Services.Busca.BuscarBandas;
using InfnetStreaming.Application.Services.Busca.BuscarMusicas;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("busca")]
    [Authorize]
    public class BuscaController : ControllerBase
    {
        [HttpGet("bandas")]
        public async Task<IActionResult> BuscarBandas(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int perPage = 10,
            [FromQuery] string orderBy = "nome",
            [FromQuery] OrdemBusca order = OrdemBusca.CRESCENTE,
            [FromServices] BuscarBandasService service = null!,
            CancellationToken cancellationToken = default)
        {
            var output = await service.Executar(
                new BuscarBandasInput(page, perPage, search ?? string.Empty, orderBy, order),
                cancellationToken);
            return Ok(output);
        }

        [HttpGet("musicas")]
        public async Task<IActionResult> BuscarMusicas(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int perPage = 10,
            [FromQuery] string orderBy = "nome",
            [FromQuery] OrdemBusca order = OrdemBusca.CRESCENTE,
            [FromServices] BuscarMusicasService service = null!,
            CancellationToken cancellationToken = default)
        {
            var output = await service.Executar(
                new BuscarMusicasInput(page, perPage, search ?? string.Empty, orderBy, order),
                cancellationToken);
            return Ok(output);
        }
    }
}
