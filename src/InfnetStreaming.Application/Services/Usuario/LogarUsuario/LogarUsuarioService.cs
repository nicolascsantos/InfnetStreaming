using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Usuario.LogarUsuario
{
    public class LogarUsuarioService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IGeradorToken _geradorToken;

        public LogarUsuarioService(IRepositorioUsuario repositorioUsuario, IGeradorToken geradorToken)
        {
            _repositorioUsuario = repositorioUsuario;
            _geradorToken = geradorToken;
        }

        public async Task<LogarUsuarioOutput> Executar(LogarUsuarioInput input, CancellationToken cancellationToken)
        {
            var usuario = await _repositorioUsuario.BuscarPorUsername(input.Username, cancellationToken)
                ?? throw new NotFoundException("Usuário ou senha inválidos.");

            if (!SenhaHelper.Verificar(input.Senha, usuario.Senha))
                throw new NotFoundException("Usuário ou senha inválidos.");

            var (token, expiraEm) = _geradorToken.Gerar(usuario.Id, usuario.Username);

            return new LogarUsuarioOutput(
                token,
                expiraEm,
                new UsuarioInfo(usuario.Id, usuario.Nome, usuario.Username)
            );
        }
    }
}
