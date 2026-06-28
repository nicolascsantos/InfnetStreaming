using InfnetStreaming.Domain.Repositorios;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Application.Services.Busca.BuscarMusicas
{
    public class BuscarMusicasService
    {
        private readonly IRepositorioMusica _repositorioMusica;

        public BuscarMusicasService(IRepositorioMusica repositorioMusica) => _repositorioMusica = repositorioMusica;

        public async Task<BuscarMusicasOutput> Executar(BuscarMusicasInput input, CancellationToken cancellationToken)
        {
            var busca = new InputBusca(input.Page, input.PerPage, input.Search, input.OrderBy, input.Order);
            var resultado = await _repositorioMusica.Buscar(busca, cancellationToken);

            var itens = resultado.Itens
                .Select(m => new MusicaOutput(m.Id, m.Nome, m.Duracao, m.OrdemMusica, m.DataCriacao))
                .ToList();

            return new BuscarMusicasOutput(resultado.PaginaAtual, resultado.QtdPorPagina, resultado.Total, itens);
        }
    }
}
