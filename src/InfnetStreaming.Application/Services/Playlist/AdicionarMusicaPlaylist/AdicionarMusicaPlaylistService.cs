using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Playlist.AdicionarMusicaPlaylist
{
    public class AdicionarMusicaPlaylistService
    {
        private readonly IRepositorioPlaylist _repositorioPlaylist;
        private readonly IRepositorioMusica _repositorioMusica;

        public AdicionarMusicaPlaylistService(IRepositorioPlaylist repositorioPlaylist, IRepositorioMusica repositorioMusica)
        {
            _repositorioPlaylist = repositorioPlaylist;
            _repositorioMusica = repositorioMusica;
        }

        public async Task Executar(AdicionarMusicaPlaylistInput input, CancellationToken cancellationToken)
        {
            await _repositorioMusica.Get(input.MusicaId, cancellationToken);
            await _repositorioPlaylist.AdicionarMusica(input.PlaylistId, input.MusicaId, cancellationToken);
        }
    }
}
