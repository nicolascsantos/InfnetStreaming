using InfnetStreaming.Application.Services.Plano.ListarPlanos;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("planos")]
    public class PlanosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromServices] ListarPlanosService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(cancellationToken);
            return Ok(output);
        }
    }
}
