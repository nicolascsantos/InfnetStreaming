namespace InfnetStreaming.Application.Services.Transacao.AutorizarTransacao
{
    public record AutorizarTransacaoInput(Guid UsuarioId, Guid PlanoId, decimal Valor);
}
