using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.SeedWork;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Domain.Repositorios
{
    public interface IRepositorioMusica : IRepositorioGenerico<Musica>, IRepositoryProcura<Musica>
    {
        Task<IReadOnlyList<Musica>> ListarPorIds(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
