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
            await _repositorioUsuario.Get(input.UsuarioId, cancellationToken);
            await _repositorioBanda.Get(input.BandaId, cancellationToken);

            if (input.Favoritar)
                await _repositorioUsuario.AdicionarBandaFavorita(input.UsuarioId, input.BandaId, cancellationToken);
            else
                await _repositorioUsuario.RemoverBandaFavorita(input.UsuarioId, input.BandaId, cancellationToken);
        }
    }
}
