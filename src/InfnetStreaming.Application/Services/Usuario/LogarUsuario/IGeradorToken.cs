namespace InfnetStreaming.Application.Services.Usuario.LogarUsuario
{
    public interface IGeradorToken
    {
        (string Token, DateTime ExpiraEm) Gerar(Guid usuarioId, string username);
    }
}
