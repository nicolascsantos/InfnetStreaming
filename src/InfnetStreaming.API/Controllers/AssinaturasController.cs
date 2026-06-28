using InfnetStreaming.Application.Services.Assinatura.AssinarPlano;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("assinaturas")]
    [Authorize]
    public class AssinaturasController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Assinar(
            [FromBody] AssinarPlanoInput input,
            [FromServices] AssinarPlanoService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(input, cancellationToken);
            return Ok(output);
        }
    }
}
