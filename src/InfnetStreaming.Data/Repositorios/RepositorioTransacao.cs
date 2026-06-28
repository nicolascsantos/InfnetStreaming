using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Enums;
using InfnetStreaming.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Repositorios
{
    public class RepositorioTransacao : IRepositorioTransacao
    {
        private readonly InfnetStreamingDbContext _context;

        public RepositorioTransacao(InfnetStreamingDbContext context) => _context = context;

        public async Task<Transacao> Adicionar(Transacao agregado, CancellationToken cancellationToken)
        {
            await _context.AddAsync(agregado, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Transacao> Get(Guid id, CancellationToken cancellationToken)
        {
            var transacao = await _context.Set<Transacao>().FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"Transação '{id}' não encontrada.");
            return transacao;
        }

        public async Task Deletar(Guid id, CancellationToken cancellationToken)
        {
            var transacao = await Get(id, cancellationToken);
            _context.Remove(transacao);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Transacao> Atualizar(Transacao agregado, CancellationToken cancellationToken)
        {
            _context.Update(agregado);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Transacao?> BuscarUltimaAprovada(Guid usuarioId, CancellationToken cancellationToken)
            => await _context.Set<Transacao>()
                .Where(t => t.UsuarioId == usuarioId && t.Status == StatusTransacao.Aprovada)
                .OrderByDescending(t => t.DataTransacao)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
