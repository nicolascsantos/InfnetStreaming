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
            var usuario = await _repositorioUsuario.GetComFavoritos(input.UsuarioId, cancellationToken);

            await _repositorioMusica.Get(input.MusicaId, cancellationToken);

            if (input.Favoritar)
                usuario.FavoritarMusica(input.MusicaId);
            else
                usuario.DesfavoritarMusica(input.MusicaId);

            await _repositorioUsuario.Atualizar(usuario, cancellationToken);
        }
    }
}
