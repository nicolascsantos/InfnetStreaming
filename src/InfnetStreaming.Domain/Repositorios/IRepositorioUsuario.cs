using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioGenerico<Usuario>
    {
        Task<Usuario?> BuscarPorUsername(string username, CancellationToken cancellationToken);
        Task<bool> ExisteUsername(string username, CancellationToken cancellationToken);
        Task<Usuario> GetComFavoritos(Guid id, CancellationToken cancellationToken);
        Task AdicionarMusicaFavorita(Guid usuarioId, Guid musicaId, CancellationToken cancellationToken);
        Task RemoverMusicaFavorita(Guid usuarioId, Guid musicaId, CancellationToken cancellationToken);
        Task AdicionarBandaFavorita(Guid usuarioId, Guid bandaId, CancellationToken cancellationToken);
        Task RemoverBandaFavorita(Guid usuarioId, Guid bandaId, CancellationToken cancellationToken);
    }
}
