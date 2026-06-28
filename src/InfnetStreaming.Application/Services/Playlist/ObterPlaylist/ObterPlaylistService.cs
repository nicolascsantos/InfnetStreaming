using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Playlist.ObterPlaylist
{
    public class ObterPlaylistService
    {
        private readonly IRepositorioPlaylist _repositorioPlaylist;

        public ObterPlaylistService(IRepositorioPlaylist repositorioPlaylist)
            => _repositorioPlaylist = repositorioPlaylist;

        public async Task<ObterPlaylistOutput> Executar(Guid playlistId, CancellationToken cancellationToken)
        {
            var playlist = await _repositorioPlaylist.GetComMusicas(playlistId, cancellationToken);

            return new ObterPlaylistOutput(
                playlist.Id,
                playlist.Nome,
                playlist.DataCriacao,
                playlist.Musicas
                    .OrderBy(m => m.OrdemMusica)
                    .Select(m => new MusicaPlaylistOutput(m.Id, m.Nome, m.Duracao, m.OrdemMusica))
                    .ToList()
            );
        }
    }
}
