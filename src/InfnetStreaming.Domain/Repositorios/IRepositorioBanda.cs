using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.SeedWork;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Domain.Repositorios
{
    public interface IRepositorioBanda : IRepositorioGenerico<Banda>, IRepositoryProcura<Banda>
    {
        Task<IReadOnlyList<Banda>> ListarPorIds(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        Task<Banda> GetComDetalhes(Guid id, CancellationToken cancellationToken);
    }
}
