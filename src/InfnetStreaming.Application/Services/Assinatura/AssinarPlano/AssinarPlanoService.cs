using InfnetStreaming.Domain.Enums;
using InfnetStreaming.Domain.Repositorios;
using TransacaoDomain = InfnetStreaming.Domain.Entities.Transacao;

namespace InfnetStreaming.Application.Services.Assinatura.AssinarPlano
{
    public class AssinarPlanoService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPlano _repositorioPlano;
        private readonly IRepositorioTransacao _repositorioTransacao;

        public AssinarPlanoService(
            IRepositorioUsuario repositorioUsuario,
            IRepositorioPlano repositorioPlano,
            IRepositorioTransacao repositorioTransacao)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlano = repositorioPlano;
            _repositorioTransacao = repositorioTransacao;
        }

        public async Task<AssinarPlanoOutput> Executar(AssinarPlanoInput input, CancellationToken cancellationToken)
        {
            var usuario = await _repositorioUsuario.Get(input.UsuarioId, cancellationToken);
            var plano = await _repositorioPlano.Get(input.PlanoId, cancellationToken);

            var transacao = new TransacaoDomain(usuario.Id, plano.Id, plano.Valor);

            var ultimaAprovada = await _repositorioTransacao.BuscarUltimaAprovada(usuario.Id, cancellationToken);
            var podeCobrar = ultimaAprovada is null
                || (DateTime.UtcNow - ultimaAprovada.DataTransacao).TotalDays >= 30;

            if (podeCobrar)
            {
                transacao.Aprovar();
                usuario.AlterarPlano(plano.Id);
                await _repositorioUsuario.Atualizar(usuario, cancellationToken);
            }
            else
            {
                transacao.Recusar();
            }

            await _repositorioTransacao.Adicionar(transacao, cancellationToken);

            return new AssinarPlanoOutput(
                transacao.Id,
                transacao.Status,
                transacao.MensagemExibicao,
                usuario.Id,
                plano.Id,
                plano.Valor
            );
        }
    }
}
