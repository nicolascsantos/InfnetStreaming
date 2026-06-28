using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.SeedWork;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Domain.Repositorios
{
    public interface IRepositorioBanda : IRepositorioGenerico<Banda>, IRepositoryProcura<Banda>
    {
    }
}
