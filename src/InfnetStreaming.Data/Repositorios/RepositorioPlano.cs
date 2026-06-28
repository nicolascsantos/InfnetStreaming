using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Repositorios
{
    public class RepositorioPlano : IRepositorioPlano
    {
        private readonly InfnetStreamingDbContext _context;

        public RepositorioPlano(InfnetStreamingDbContext context) => _context = context;

        public async Task<Plano> Adicionar(Plano agregado, CancellationToken cancellationToken)
        {
            await _context.AddAsync(agregado, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Plano> Get(Guid id, CancellationToken cancellationToken)
        {
            var plano = await _context.Set<Plano>().FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"Plano '{id}' não encontrado.");
            return plano;
        }

        public async Task Deletar(Guid id, CancellationToken cancellationToken)
        {
            var plano = await Get(id, cancellationToken);
            _context.Remove(plano);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Plano> Atualizar(Plano agregado, CancellationToken cancellationToken)
        {
            if (_context.Entry(agregado).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                _context.Update(agregado);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<IReadOnlyList<Plano>> ListarTodos(CancellationToken cancellationToken)
            => await _context.Set<Plano>().AsNoTracking().ToListAsync(cancellationToken);
    }
}
