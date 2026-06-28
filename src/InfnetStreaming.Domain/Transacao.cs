using InfnetStreaming.Domain.Enums;
using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Transacao : RaizDeAgregacao
    {
        public Guid UsuarioId { get; private set; }
        public Guid PlanoId { get; private set; }
        public decimal Valor { get; private set; }
        public StatusTransacao Status { get; private set; }
        public DateTime DataTransacao { get; private set; }

        public string MensagemExibicao => Status.MensagemExibicao();

        public Transacao(Guid usuarioId, Guid planoId, decimal valor)
        {
            UsuarioId = usuarioId;
            PlanoId = planoId;
            Valor = valor;
            Status = StatusTransacao.Pendente;
            DataTransacao = DateTime.Now;
        }

        public void Aprovar() => Status = StatusTransacao.Aprovada;
        public void Recusar() => Status = StatusTransacao.Recusada;
        public void Cancelar() => Status = StatusTransacao.Cancelada;

        protected Transacao() { }
    }
}
