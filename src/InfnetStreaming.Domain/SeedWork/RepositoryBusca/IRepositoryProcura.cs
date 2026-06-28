namespace InfnetStreaming.Domain.SeedWork.RepositoryBusca
{
    public interface IRepositoryProcura<TAgregado> where TAgregado : RaizDeAgregacao
    {
        Task<RespostaBusca<TAgregado>> Buscar(InputBusca input, CancellationToken cancellationToken);
    }
}
