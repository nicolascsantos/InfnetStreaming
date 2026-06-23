using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class IntegranteConfiguration : IEntityTypeConfiguration<Integrante>
    {
        public void Configure(EntityTypeBuilder<Integrante> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.DataDeNascimento).IsRequired();
        }
    }
}
