namespace InfnetStreaming.Application.Services.Playlist.ObterPlaylist
{
    public record ObterPlaylistOutput(
        Guid Id,
        string Nome,
        DateTime DataCriacao,
        List<MusicaPlaylistOutput> Musicas
    );

    public record MusicaPlaylistOutput(Guid Id, string Nome, TimeSpan Duracao, int OrdemMusica);
}
