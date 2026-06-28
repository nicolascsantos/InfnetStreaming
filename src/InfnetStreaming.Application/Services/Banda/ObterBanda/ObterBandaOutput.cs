namespace InfnetStreaming.Application.Services.Banda.ObterBanda
{
    public record ObterBandaOutput(
        Guid Id,
        string Nome,
        DateTime DataFormacao,
        List<IntegranteOutput> Integrantes,
        List<string> Generos,
        List<AlbumOutput> Albuns,
        List<SingleOutput> Singles
    );

    public record IntegranteOutput(Guid Id, string Nome, DateTime DataDeNascimento);

    public record AlbumOutput(
        Guid Id,
        string Nome,
        DateTime DataLancamento,
        List<MusicaAlbumOutput> Musicas
    );

    public record MusicaAlbumOutput(Guid Id, string Nome, TimeSpan Duracao, int OrdemMusica);

    public record SingleOutput(Guid Id, string Nome, TimeSpan Duracao);
}
