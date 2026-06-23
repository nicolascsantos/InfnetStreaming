using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.DataLancamento).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasMany(a => a.Musicas)
                .WithOne()
                .HasForeignKey("AlbumId");

            builder.Navigation(a => a.Musicas).HasField("_musicas");
        }
    }
}
