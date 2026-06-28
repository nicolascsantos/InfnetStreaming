namespace InfnetStreaming.Domain.SeedWork.RepositoryBusca
{
    public class RespostaBusca<TAgregado> where TAgregado : RaizDeAgregacao
    {
        public RespostaBusca(int paginaAtual, int qtdPorPagina, int total, IReadOnlyList<TAgregado> itens)
        {
            PaginaAtual = paginaAtual;
            QtdPorPagina = qtdPorPagina;
            Itens = itens;
            Total = total;
        }

        public int PaginaAtual { get; set; }

        public int QtdPorPagina { get; set; }

        public int Total { get; set; }

        public IReadOnlyList<TAgregado> Itens { get; set; }
    }
}
