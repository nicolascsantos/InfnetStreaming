using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Usuario.FavoritarMusica
{
    public class FavoritarMusicaService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioMusica _repositorioMusica;

        public FavoritarMusicaService(IRepositorioUsuario repositorioUsuario, IRepositorioMusica repositorioMusica)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioMusica = repositorioMusica;
        }

        public async Task Executar(FavoritarMusicaInput input, CancellationToken cancellationToken)
        {
            await _repositorioUsuario.Get(input.UsuarioId, cancellationToken);
            await _repositorioMusica.Get(input.MusicaId, cancellationToken);

            if (input.Favoritar)
                await _repositorioUsuario.AdicionarMusicaFavorita(input.UsuarioId, input.MusicaId, cancellationToken);
            else
                await _repositorioUsuario.RemoverMusicaFavorita(input.UsuarioId, input.MusicaId, cancellationToken);
        }
    }
}
