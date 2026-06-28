namespace InfnetStreaming.Application.Services.Busca.BuscarMusicas
{
    public record BuscarMusicasOutput(
        int PaginaAtual,
        int QtdPorPagina,
        int Total,
        IReadOnlyList<MusicaOutput> Itens
    );

    public record MusicaOutput(Guid Id, string Nome, TimeSpan Duracao, int OrdemMusica, DateTime DataCriacao);
}
