using InfnetStreaming.Domain.Enums;

namespace InfnetStreaming.Application.Services.Assinatura.AssinarPlano
{
    public record AssinarPlanoOutput(
        Guid TransacaoId,
        StatusTransacao Status,
        string MensagemExibicao,
        Guid UsuarioId,
        Guid PlanoId,
        decimal Valor
    );
}
