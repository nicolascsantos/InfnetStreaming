using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasMany(p => p.Musicas)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PlaylistMusica",
                    j => j.HasOne<Musica>().WithMany().HasForeignKey("MusicaId"),
                    j => j.HasOne<Playlist>().WithMany().HasForeignKey("PlaylistId")
                );

            builder.Navigation(p => p.Musicas).HasField("_musicas");
        }
    }
}
