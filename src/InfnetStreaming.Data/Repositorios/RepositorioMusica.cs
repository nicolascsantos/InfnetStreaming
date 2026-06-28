using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Repositorios;
using InfnetStreaming.Domain.SeedWork.RepositoryBusca;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Repositorios
{
    public class RepositorioMusica : IRepositorioMusica
    {
        private readonly InfnetStreamingDbContext _context;

        public RepositorioMusica(InfnetStreamingDbContext context) => _context = context;

        public async Task<Musica> Adicionar(Musica agregado, CancellationToken cancellationToken)
        {
            await _context.AddAsync(agregado, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Musica> Get(Guid id, CancellationToken cancellationToken)
        {
            var musica = await _context.Set<Musica>().FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"Música '{id}' não encontrada.");
            return musica;
        }

        public async Task Deletar(Guid id, CancellationToken cancellationToken)
        {
            var musica = await Get(id, cancellationToken);
            _context.Remove(musica);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Musica> Atualizar(Musica agregado, CancellationToken cancellationToken)
        {
            if (_context.Entry(agregado).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                _context.Update(agregado);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<RespostaBusca<Musica>> Buscar(InputBusca input, CancellationToken cancellationToken)
        {
            var query = _context.Set<Musica>().AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(m => EF.Functions.Like(m.Nome, $"%{input.Search}%"));

            query = input.OrderBy?.ToLowerInvariant() switch
            {
                "data" => input.Order == OrdemBusca.CRESCENTE
                    ? query.OrderBy(m => m.DataCriacao)
                    : query.OrderByDescending(m => m.DataCriacao),
                "duracao" => input.Order == OrdemBusca.CRESCENTE
                    ? query.OrderBy(m => m.Duracao)
                    : query.OrderByDescending(m => m.Duracao),
                _ => input.Order == OrdemBusca.CRESCENTE
                    ? query.OrderBy(m => m.Nome)
                    : query.OrderByDescending(m => m.Nome)
            };

            var total = await query.CountAsync(cancellationToken);
            var itens = await query
                .Skip((input.Page - 1) * input.PerPage)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);

            return new RespostaBusca<Musica>(input.Page, input.PerPage, total, itens);
        }

        public async Task<IReadOnlyList<Musica>> ListarPorIds(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => await _context.Set<Musica>().AsNoTracking()
                .Where(m => ids.Contains(m.Id))
                .ToListAsync(cancellationToken);
    }
}
