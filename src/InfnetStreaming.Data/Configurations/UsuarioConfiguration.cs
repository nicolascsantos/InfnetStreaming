using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Username)
                .IsRequired();


            builder.Property(x => x.Senha)
                .IsRequired();

            builder.Property(x => x.DataCriada)
                .IsRequired();

            builder.HasMany(u => u.Playlists)
                .WithOne()
                .HasForeignKey("UsuarioId");

            builder.Navigation(u => u.Playlists).HasField("_playlists");

            builder.HasMany(u => u.MusicasFavoritas)
                .WithOne()
                .HasForeignKey("UsuarioId");

            builder.Navigation(u => u.MusicasFavoritas).HasField("_musicasFavoritas");
        }
    }
}
