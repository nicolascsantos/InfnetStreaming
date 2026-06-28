using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class BandaConfiguration : IEntityTypeConfiguration<Banda>
    {
        public void Configure(EntityTypeBuilder<Banda> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome)
                .IsRequired();
            builder.Property(x => x.DataFormacao)
                .IsRequired();
            builder.HasIndex(x => x.Nome);

            builder.HasMany(b => b.Albuns)
                .WithOne()
                .HasForeignKey("BandaId");

            builder.HasMany(b => b.Generos)
                .WithOne()
                .HasForeignKey("BandaId");

            builder.HasMany(b => b.Integrantes)
                .WithOne()
                .HasForeignKey("BandaId");

            builder.HasMany(b => b.Singles)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "MusicaBanda",
                    j => j.HasOne<Musica>().WithMany().HasForeignKey("MusicaId"),
                    j => j.HasOne<Banda>().WithMany().HasForeignKey("BandaId")
                );

            builder.Navigation(x => x.Albuns).HasField("_albuns");
            builder.Navigation(x => x.Generos).HasField("_generos");
            builder.Navigation(x => x.Integrantes).HasField("_integrantes");
            builder.Navigation(x => x.Singles).HasField("_singles");

        }
    }
}
