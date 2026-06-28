using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Playlist.ListarPlaylists
{
    public class ListarPlaylistsService
    {
        private readonly IRepositorioPlaylist _repositorioPlaylist;

        public ListarPlaylistsService(IRepositorioPlaylist repositorioPlaylist)
            => _repositorioPlaylist = repositorioPlaylist;

        public async Task<List<ListarPlaylistsOutput>> Executar(Guid usuarioId, CancellationToken cancellationToken)
        {
            var playlists = await _repositorioPlaylist.ListarPorUsuario(usuarioId, cancellationToken);
            return playlists
                .Select(p => new ListarPlaylistsOutput(p.Id, p.Nome, p.DataCriacao))
                .ToList();
        }
    }
}
