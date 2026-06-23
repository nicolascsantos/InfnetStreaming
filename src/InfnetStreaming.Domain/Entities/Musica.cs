using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Musica : Entidade
    {
        public string Nome { get; private set; }

        public TimeSpan Duracao { get; private set; }

        public IReadOnlyCollection<Guid> BandaIds { get; private set; }

        public int OrdemMusica { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public Musica(
            string nome,
            TimeSpan duracao,
            List<Guid> bandaIds,
            int ordemMusica
        )
        {
            Nome = nome;
            Duracao = duracao;
            BandaIds = bandaIds;
            OrdemMusica = ordemMusica;
            DataCriacao = DateTime.Now;
        }

        protected Musica() { Nome = null!; BandaIds = null!; }
    }
}
