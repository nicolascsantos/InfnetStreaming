using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class BandaFavoritaConfiguration : IEntityTypeConfiguration<BandaFavorita>
    {
        public void Configure(EntityTypeBuilder<BandaFavorita> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BandaId).IsRequired();
            builder.Property(x => x.DataFavoritado).IsRequired();
        }
    }
}
