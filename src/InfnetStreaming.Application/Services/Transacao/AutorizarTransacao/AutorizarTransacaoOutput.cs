using InfnetStreaming.Domain.Enums;

namespace InfnetStreaming.Application.Services.Transacao.AutorizarTransacao
{
    public record AutorizarTransacaoOutput(
        Guid TransacaoId,
        StatusTransacao Status,
        string MensagemExibicao,
        decimal Valor,
        DateTime DataTransacao
    );
}
