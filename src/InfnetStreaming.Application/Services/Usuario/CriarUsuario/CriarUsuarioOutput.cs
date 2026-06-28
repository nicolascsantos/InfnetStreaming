namespace InfnetStreaming.Application.Services.Usuario.CriarUsuario
{
    public record CriarUsuarioOutput(Guid Id, string Nome, string Username, Guid PlanoId, DateTime DataCriada);
}
