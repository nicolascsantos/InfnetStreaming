namespace InfnetStreaming.Application.Services.Usuario.ObterUsuario
{
    public record ObterUsuarioOutput(
        Guid Id, string Nome, string Username,
        Guid PlanoId, string NomePlano, decimal ValorPlano,
        DateTime DataCriada
    );
}
