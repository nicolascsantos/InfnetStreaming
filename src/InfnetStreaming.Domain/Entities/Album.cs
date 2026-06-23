using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Album : Entidade
    {
        private readonly List<Musica> _musicas = new();

        public string Nome { get; private set; }

        public IReadOnlyCollection<Musica> Musicas => _musicas;

        public DateTime DataLancamento { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public Album(
            string nome,
            DateTime dataLancamento
        )
        {
            Nome = nome;
            DataLancamento = dataLancamento;
            DataCriacao = DateTime.Now;
        }

        public void AdicionarMusica(Musica musica) => _musicas.Add(musica);

        protected Album() { Nome = null!; }
    }
}
