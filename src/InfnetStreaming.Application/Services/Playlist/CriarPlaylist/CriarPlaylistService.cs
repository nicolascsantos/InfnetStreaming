using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Playlist.CriarPlaylist
{
    public class CriarPlaylistService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPlaylist _repositorioPlaylist;

        public CriarPlaylistService(IRepositorioUsuario repositorioUsuario, IRepositorioPlaylist repositorioPlaylist)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlaylist = repositorioPlaylist;
        }

        public async Task<CriarPlaylistOutput> Executar(CriarPlaylistInput input, CancellationToken cancellationToken)
        {
            await _repositorioUsuario.Get(input.UsuarioId, cancellationToken);

            var playlist = new Domain.Entities.Playlist(input.Nome);
            await _repositorioPlaylist.Adicionar(playlist, input.UsuarioId, cancellationToken);

            return new CriarPlaylistOutput(playlist.Id, playlist.Nome, playlist.DataCriacao);
        }
    }
}
