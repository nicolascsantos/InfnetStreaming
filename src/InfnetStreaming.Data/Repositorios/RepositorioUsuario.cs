using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly InfnetStreamingDbContext _context;

        public RepositorioUsuario(InfnetStreamingDbContext context) => _context = context;

        public async Task<Usuario> Adicionar(Usuario agregado, CancellationToken cancellationToken)
        {
            await _context.AddAsync(agregado, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Usuario> Get(Guid id, CancellationToken cancellationToken)
        {
            var usuario = await _context.Set<Usuario>().FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"Usuário '{id}' não encontrado.");
            return usuario;
        }

        public async Task<Usuario> GetComFavoritos(Guid id, CancellationToken cancellationToken)
        {
            var usuario = await _context.Set<Usuario>()
                .Include(u => u.MusicasFavoritas)
                .Include(u => u.BandasFavoritas)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
                ?? throw new NotFoundException($"Usuário '{id}' não encontrado.");
            return usuario;
        }

        public async Task Deletar(Guid id, CancellationToken cancellationToken)
        {
            var usuario = await Get(id, cancellationToken);
            _context.Remove(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Usuario> Atualizar(Usuario agregado, CancellationToken cancellationToken)
        {
            _context.Update(agregado);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Usuario?> BuscarPorUsername(string username, CancellationToken cancellationToken)
            => await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

        public async Task<bool> ExisteUsername(string username, CancellationToken cancellationToken)
            => await _context.Set<Usuario>().AnyAsync(u => u.Username == username, cancellationToken);
    }
}
