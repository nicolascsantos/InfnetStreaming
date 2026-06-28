using InfnetStreaming.Domain.Repositorios;
using TransacaoDomain = InfnetStreaming.Domain.Entities.Transacao;

namespace InfnetStreaming.Application.Services.Transacao.AutorizarTransacao
{
    public class AutorizarTransacaoService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPlano _repositorioPlano;
        private readonly IRepositorioTransacao _repositorioTransacao;

        public AutorizarTransacaoService(
            IRepositorioUsuario repositorioUsuario,
            IRepositorioPlano repositorioPlano,
            IRepositorioTransacao repositorioTransacao)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlano = repositorioPlano;
            _repositorioTransacao = repositorioTransacao;
        }

        public async Task<AutorizarTransacaoOutput> Executar(AutorizarTransacaoInput input, CancellationToken cancellationToken)
        {
            var usuario = await _repositorioUsuario.Get(input.UsuarioId, cancellationToken);
            var plano = await _repositorioPlano.Get(input.PlanoId, cancellationToken);

            var transacao = new TransacaoDomain(usuario.Id, plano.Id, input.Valor);

            if (input.Valor != plano.Valor)
            {
                transacao.Recusar();
                await _repositorioTransacao.Adicionar(transacao, cancellationToken);
                return ToOutput(transacao);
            }

            var ultimaAprovada = await _repositorioTransacao.BuscarUltimaAprovada(usuario.Id, cancellationToken);
            var jaAutorizadaRecentemente = ultimaAprovada is not null
                && ultimaAprovada.PlanoId == plano.Id
                && (DateTime.UtcNow - ultimaAprovada.DataTransacao).TotalDays < 30;

            if (jaAutorizadaRecentemente)
                transacao.Recusar();
            else
                transacao.Aprovar();

            await _repositorioTransacao.Adicionar(transacao, cancellationToken);
            return ToOutput(transacao);
        }

        private static AutorizarTransacaoOutput ToOutput(TransacaoDomain t)
            => new(t.Id, t.Status, t.MensagemExibicao, t.Valor, t.DataTransacao);
    }
}
