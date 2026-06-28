namespace InfnetStreaming.Application.Services.Busca.BuscarBandas
{
    public record BuscarBandasOutput(
        int PaginaAtual,
        int QtdPorPagina,
        int Total,
        IReadOnlyList<BandaOutput> Itens
    );

    public record BandaOutput(Guid Id, string Nome, DateTime DataFormacao, DateTime DataCriacao);
}
