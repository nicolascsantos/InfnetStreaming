using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Usuario.ObterUsuario
{
    public class ObterUsuarioService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPlano _repositorioPlano;

        public ObterUsuarioService(IRepositorioUsuario repositorioUsuario, IRepositorioPlano repositorioPlano)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlano = repositorioPlano;
        }

        public async Task<ObterUsuarioOutput> Executar(Guid id, CancellationToken cancellationToken)
        {
            var usuario = await _repositorioUsuario.Get(id, cancellationToken);
            var plano = await _repositorioPlano.Get(usuario.PlanoId, cancellationToken);

            return new ObterUsuarioOutput(
                usuario.Id, usuario.Nome, usuario.Username,
                plano.Id, plano.Nome, plano.Valor,
                usuario.DataCriada
            );
        }
    }
}
