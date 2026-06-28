namespace InfnetStreaming.Application.Services.Usuario.FavoritarBanda
{
    public record FavoritarBandaInput(Guid UsuarioId, Guid BandaId, bool Favoritar);
}
