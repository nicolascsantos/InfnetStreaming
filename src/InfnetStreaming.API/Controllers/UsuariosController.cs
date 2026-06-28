using InfnetStreaming.Application.Services.Usuario.CriarUsuario;
using InfnetStreaming.Application.Services.Usuario.LogarUsuario;
using Microsoft.AspNetCore.Mvc;

namespace InfnetStreaming.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Criar(
            [FromBody] CriarUsuarioInput input,
            [FromServices] CriarUsuarioService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(input, cancellationToken);
            return CreatedAtAction(nameof(Criar), new { id = output.Id }, output);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LogarUsuarioInput input,
            [FromServices] LogarUsuarioService service,
            CancellationToken cancellationToken)
        {
            var output = await service.Executar(input, cancellationToken);
            return Ok(output);
        }
    }
}
