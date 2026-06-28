namespace InfnetStreaming.Application.Services.Usuario.ObterFavoritos
{
    public record ObterFavoritosOutput(
        IReadOnlyList<MusicaFavoritaOutput> MusicasFavoritas,
        IReadOnlyList<BandaFavoritaOutput> BandasFavoritas
    );

    public record MusicaFavoritaOutput(Guid MusicaId, string Nome, TimeSpan Duracao);
    public record BandaFavoritaOutput(Guid BandaId, string Nome);
}
