using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class MusicaConfiguration : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Duracao).IsRequired();
            builder.Property(x => x.OrdemMusica).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasMany<Banda>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "MusicaBanda",
                    j => j.HasOne<Banda>().WithMany().HasForeignKey("BandaId"),
                    j => j.HasOne<Musica>().WithMany().HasForeignKey("MusicaId")
                );

            builder.Ignore(x => x.BandaIds);
        }
    }
}
