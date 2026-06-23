using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Playlist : Entidade
    {
        private readonly List<Musica> _musicas = new();

        public string Nome { get; private set; }

        public IReadOnlyCollection<Musica> Musicas => _musicas;

        public DateTime DataCriacao { get; private set; }

        public Playlist(string nome)
        {
            Nome = nome;
            DataCriacao = DateTime.Now;
        }

        public void AdicionarMusica(Musica musica) => _musicas.Add(musica);

        protected Playlist() { Nome = null!; }
    }
}
