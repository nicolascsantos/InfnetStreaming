using InfnetStreaming.Application.Services.Banda.ObterBanda;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("bandas")]
    [Authorize]
    public class BandasController : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterBanda(
            Guid id,
            [FromServices] ObterBandaService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(id, cancellationToken);
            return Ok(output);
        }
    }
}
