namespace InfnetStreaming.Domain.Enums
{
    public enum StatusTransacao
    {
        Pendente,
        Aprovada,
        Recusada,
        Cancelada
    }

    public static class StatusTransacaoExtensions
    {
        public static string MensagemExibicao(this StatusTransacao status) => status switch
        {
            StatusTransacao.Pendente  => "Aguardando confirmação do pagamento.",
            StatusTransacao.Aprovada  => "Pagamento aprovado. Aproveite sua assinatura!",
            StatusTransacao.Recusada  => "Pagamento recusado. Verifique seus dados e tente novamente.",
            StatusTransacao.Cancelada => "Assinatura cancelada.",
            _                         => "Status desconhecido."
        };
    }
}
