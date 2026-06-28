using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Usuario.FavoritarBanda
{
    public class FavoritarBandaService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioBanda _repositorioBanda;

        public FavoritarBandaService(IRepositorioUsuario repositorioUsuario, IRepositorioBanda repositorioBanda)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioBanda = repositorioBanda;
        }

        public async Task Executar(FavoritarBandaInput input, CancellationToken cancellationToken)
        {
            var usuario = await _repositorioUsuario.GetComFavoritos(input.UsuarioId, cancellationToken);

            await _repositorioBanda.Get(input.BandaId, cancellationToken);

            if (input.Favoritar)
                usuario.FavoritarBanda(input.BandaId);
            else
                usuario.DesfavoritarBanda(input.BandaId);

            await _repositorioUsuario.Atualizar(usuario, cancellationToken);
        }
    }
}
