using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Repositorios;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Repositorios
{
    public class RepositorioBanda : IRepositorioBanda
    {
        private readonly InfnetStreamingDbContext _context;

        public RepositorioBanda(InfnetStreamingDbContext context) => _context = context;

        public async Task<Banda> Adicionar(Banda agregado, CancellationToken cancellationToken)
        {
            await _context.AddAsync(agregado, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Banda> Get(Guid id, CancellationToken cancellationToken)
        {
            var banda = await _context.Set<Banda>().FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"Banda '{id}' não encontrada.");
            return banda;
        }

        public async Task Deletar(Guid id, CancellationToken cancellationToken)
        {
            var banda = await Get(id, cancellationToken);
            _context.Remove(banda);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Banda> Atualizar(Banda agregado, CancellationToken cancellationToken)
        {
            if (_context.Entry(agregado).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                _context.Update(agregado);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<RespostaBusca<Banda>> Buscar(InputBusca input, CancellationToken cancellationToken)
        {
            var query = _context.Set<Banda>().AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(b => EF.Functions.Like(b.Nome, $"%{input.Search}%"));

            query = input.OrderBy?.ToLowerInvariant() switch
            {
                "data" => input.Order == OrdemBusca.CRESCENTE
                    ? query.OrderBy(b => b.DataCriacao)
                    : query.OrderByDescending(b => b.DataCriacao),
                _ => input.Order == OrdemBusca.CRESCENTE
                    ? query.OrderBy(b => b.Nome)
                    : query.OrderByDescending(b => b.Nome)
            };

            var total = await query.CountAsync(cancellationToken);
            var itens = await query
                .Skip((input.Page - 1) * input.PerPage)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);

            return new RespostaBusca<Banda>(input.Page, input.PerPage, total, itens);
        }

        public async Task<IReadOnlyList<Banda>> ListarPorIds(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => await _context.Set<Banda>().AsNoTracking()
                .Where(b => ids.Contains(b.Id))
                .ToListAsync(cancellationToken);

        public async Task<Banda> GetComDetalhes(Guid id, CancellationToken cancellationToken)
        {
            var banda = await _context.Set<Banda>()
                .Include(b => b.Albuns).ThenInclude(a => a.Musicas)
                .Include(b => b.Integrantes)
                .Include(b => b.Generos)
                .Include(b => b.Singles)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken)
                ?? throw new NotFoundException($"Banda '{id}' não encontrada.");
            return banda;
        }
    }
}
