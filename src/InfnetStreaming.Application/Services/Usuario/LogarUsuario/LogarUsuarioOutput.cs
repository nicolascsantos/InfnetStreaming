namespace InfnetStreaming.Application.Services.Usuario.LogarUsuario
{
    public record LogarUsuarioOutput(string Token, DateTime ExpiraEm, UsuarioInfo Usuario);

    public record UsuarioInfo(Guid Id, string Nome, string Username);
}
