using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Playlist.RemoverMusicaPlaylist
{
    public class RemoverMusicaPlaylistService
    {
        private readonly IRepositorioPlaylist _repositorioPlaylist;

        public RemoverMusicaPlaylistService(IRepositorioPlaylist repositorioPlaylist)
            => _repositorioPlaylist = repositorioPlaylist;

        public async Task Executar(RemoverMusicaPlaylistInput input, CancellationToken cancellationToken)
            => await _repositorioPlaylist.RemoverMusica(input.PlaylistId, input.MusicaId, cancellationToken);
    }
}
