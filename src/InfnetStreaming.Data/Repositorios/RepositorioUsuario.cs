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
            if (_context.Entry(agregado).State == EntityState.Detached)
                _context.Update(agregado);
            await _context.SaveChangesAsync(cancellationToken);
            return agregado;
        }

        public async Task<Usuario?> BuscarPorUsername(string username, CancellationToken cancellationToken)
            => await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

        public async Task<bool> ExisteUsername(string username, CancellationToken cancellationToken)
            => await _context.Set<Usuario>().AnyAsync(u => u.Username == username, cancellationToken);

        public async Task AdicionarMusicaFavorita(Guid usuarioId, Guid musicaId, CancellationToken cancellationToken)
        {
            var jaExiste = await _context.Set<MusicaFavorita>()
                .AnyAsync(f => EF.Property<Guid>(f, "UsuarioId") == usuarioId && f.MusicaId == musicaId, cancellationToken);
            if (jaExiste) return;
            var favorita = new MusicaFavorita(musicaId, DateTime.UtcNow);
            _context.Add(favorita);
            _context.Entry(favorita).Property("UsuarioId").CurrentValue = usuarioId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoverMusicaFavorita(Guid usuarioId, Guid musicaId, CancellationToken cancellationToken)
        {
            var favorita = await _context.Set<MusicaFavorita>()
                .FirstOrDefaultAsync(f => EF.Property<Guid>(f, "UsuarioId") == usuarioId && f.MusicaId == musicaId, cancellationToken);
            if (favorita is null) return;
            _context.Remove(favorita);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AdicionarBandaFavorita(Guid usuarioId, Guid bandaId, CancellationToken cancellationToken)
        {
            var jaExiste = await _context.Set<BandaFavorita>()
                .AnyAsync(f => EF.Property<Guid>(f, "UsuarioId") == usuarioId && f.BandaId == bandaId, cancellationToken);
            if (jaExiste) return;
            var favorita = new BandaFavorita(bandaId);
            _context.Add(favorita);
            _context.Entry(favorita).Property("UsuarioId").CurrentValue = usuarioId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoverBandaFavorita(Guid usuarioId, Guid bandaId, CancellationToken cancellationToken)
        {
            var favorita = await _context.Set<BandaFavorita>()
                .FirstOrDefaultAsync(f => EF.Property<Guid>(f, "UsuarioId") == usuarioId && f.BandaId == bandaId, cancellationToken);
            if (favorita is null) return;
            _context.Remove(favorita);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
