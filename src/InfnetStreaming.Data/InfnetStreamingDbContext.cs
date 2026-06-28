using InfnetStreaming.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data
{
    public class InfnetStreamingDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BandaConfiguration());
            modelBuilder.ApplyConfiguration(new IntegranteConfiguration());
            modelBuilder.ApplyConfiguration(new MusicaConfiguration());
            modelBuilder.ApplyConfiguration(new GeneroConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new PlanoConfiguration());
            modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
        }

        public InfnetStreamingDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
