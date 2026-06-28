namespace InfnetStreaming.Domain.SeedWork
{
    public interface IRepositorioGenerico<TAgregado> : IRepository where TAgregado : RaizDeAgregacao
    {
        public Task<TAgregado> Adicionar(TAgregado agregado, CancellationToken cancellationToken);

        public Task<TAgregado> Get(Guid id, CancellationToken cancellationToken);

        public Task Deletar(Guid id, CancellationToken cancellationToken);

        public Task<TAgregado> Atualizar(TAgregado agregado, CancellationToken cancellationToken);
    }
}
