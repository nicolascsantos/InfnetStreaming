using InfnetStreaming.Application.Services.Transacao.AutorizarTransacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("transacoes")]
    [Authorize]
    public class TransacoesController : ControllerBase
    {
        [HttpPost("autorizar")]
        public async Task<IActionResult> Autorizar(
            [FromBody] AutorizarTransacaoInput input,
            [FromServices] AutorizarTransacaoService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(input, cancellationToken);
            return Ok(output);
        }
    }
}
