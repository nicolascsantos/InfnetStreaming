using InfnetStreaming.Domain.Entities;

namespace InfnetStreaming.Domain.Repositorios
{
    public interface IRepositorioPlaylist
    {
        Task<Playlist> Adicionar(Playlist playlist, Guid usuarioId, CancellationToken cancellationToken);
        Task<Playlist> GetComMusicas(Guid id, CancellationToken cancellationToken);
        Task Deletar(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Playlist>> ListarPorUsuario(Guid usuarioId, CancellationToken cancellationToken);
        Task AdicionarMusica(Guid playlistId, Guid musicaId, CancellationToken cancellationToken);
        Task RemoverMusica(Guid playlistId, Guid musicaId, CancellationToken cancellationToken);
    }
}
