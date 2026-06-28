using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfnetStreaming.Data.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.PlanoId).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.DataTransacao).IsRequired();
            builder.Ignore(x => x.MensagemExibicao);
        }
    }
}
