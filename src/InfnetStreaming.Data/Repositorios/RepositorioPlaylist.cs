using InfnetStreaming.Domain.Entities;
using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Repositorios
{
    public class RepositorioPlaylist : IRepositorioPlaylist
    {
        private readonly InfnetStreamingDbContext _context;

        public RepositorioPlaylist(InfnetStreamingDbContext context) => _context = context;

        public async Task<Playlist> Adicionar(Playlist playlist, Guid usuarioId, CancellationToken cancellationToken)
        {
            _context.Add(playlist);
            _context.Entry(playlist).Property("UsuarioId").CurrentValue = usuarioId;
            await _context.SaveChangesAsync(cancellationToken);
            return playlist;
        }

        public async Task<Playlist> GetComMusicas(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Playlist>()
                .Include(p => p.Musicas)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new NotFoundException($"Playlist '{id}' não encontrada.");
        }

        public async Task Deletar(Guid id, CancellationToken cancellationToken)
        {
            var playlist = await _context.Set<Playlist>().FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"Playlist '{id}' não encontrada.");
            _context.Remove(playlist);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Playlist>> ListarPorUsuario(Guid usuarioId, CancellationToken cancellationToken)
        {
            return await _context.Set<Playlist>()
                .Where(p => EF.Property<Guid>(p, "UsuarioId") == usuarioId)
                .AsNoTracking()
                .OrderBy(p => p.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task AdicionarMusica(Guid playlistId, Guid musicaId, CancellationToken cancellationToken)
        {
            var jaExiste = await _context.Set<Dictionary<string, object>>("PlaylistMusica")
                .AnyAsync(e => EF.Property<Guid>(e, "PlaylistId") == playlistId
                            && EF.Property<Guid>(e, "MusicaId") == musicaId, cancellationToken);
            if (jaExiste) return;

            _context.Set<Dictionary<string, object>>("PlaylistMusica").Add(new Dictionary<string, object>
            {
                ["PlaylistId"] = playlistId,
                ["MusicaId"] = musicaId
            });
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoverMusica(Guid playlistId, Guid musicaId, CancellationToken cancellationToken)
        {
            var entry = await _context.Set<Dictionary<string, object>>("PlaylistMusica")
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "PlaylistId") == playlistId
                                       && EF.Property<Guid>(e, "MusicaId") == musicaId, cancellationToken);
            if (entry is null) return;
            _context.Set<Dictionary<string, object>>("PlaylistMusica").Remove(entry);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
