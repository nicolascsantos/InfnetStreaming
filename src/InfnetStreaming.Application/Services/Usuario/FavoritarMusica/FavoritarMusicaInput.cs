namespace InfnetStreaming.Application.Services.Usuario.FavoritarMusica
{
    public record FavoritarMusicaInput(Guid UsuarioId, Guid MusicaId, bool Favoritar);
}
