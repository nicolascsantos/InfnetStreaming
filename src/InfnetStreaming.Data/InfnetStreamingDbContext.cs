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
        }

        public InfnetStreamingDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
