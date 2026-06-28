using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Repositorios
{
    public interface IRepositorioTransacao : IRepositorioGenerico<Transacao>
    {
        Task<Transacao?> BuscarUltimaAprovada(Guid usuarioId, CancellationToken cancellationToken);
    }
}
