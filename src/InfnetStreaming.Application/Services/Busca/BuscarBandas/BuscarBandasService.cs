using InfnetStreaming.Domain.Repositorios;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Application.Services.Busca.BuscarBandas
{
    public class BuscarBandasService
    {
        private readonly IRepositorioBanda _repositorioBanda;

        public BuscarBandasService(IRepositorioBanda repositorioBanda) => _repositorioBanda = repositorioBanda;

        public async Task<BuscarBandasOutput> Executar(BuscarBandasInput input, CancellationToken cancellationToken)
        {
            var busca = new InputBusca(input.Page, input.PerPage, input.Search, input.OrderBy, input.Order);
            var resultado = await _repositorioBanda.Buscar(busca, cancellationToken);

            var itens = resultado.Itens
                .Select(b => new BandaOutput(b.Id, b.Nome, b.DataFormacao, b.DataCriacao))
                .ToList();

            return new BuscarBandasOutput(resultado.PaginaAtual, resultado.QtdPorPagina, resultado.Total, itens);
        }
    }
}
