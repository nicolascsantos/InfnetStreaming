using InfnetStreaming.Domain.SeedWork;
using InfnetStreaming.Domain.Validacao;

namespace InfnetStreaming.Domain.Entities
{
    public class Banda : RaizDeAgregacao
    {
        private readonly List<Genero> _generos = new();
        private readonly List<Integrante> _integrantes = new();
        private readonly List<Album> _albuns = new();
        private readonly List<Musica> _singles = new();

        public string Nome { get; private set; }

        public DateTime DataFormacao { get; private set; }

        public IReadOnlyCollection<Genero> Generos => _generos;

        public IReadOnlyCollection<Integrante> Integrantes => _integrantes;

        public IReadOnlyCollection<Album> Albuns => _albuns;

        public IReadOnlyCollection<Musica> Singles => _singles;

        public DateTime DataCriacao { get; private set; }

        public Banda(
            string nome,
            DateTime dataFormacao
        )
        {
            Nome = nome;
            DataFormacao = dataFormacao;
            DataCriacao = DateTime.Now;
            Validar();
        }

        public void AdicionarAlbum(Album album) => _albuns.Add(album);

        public void AdicionarIntegrante(Integrante integrante) => _integrantes.Add(integrante);

        public void AdicionarGenero(Genero genero) => _generos.Add(genero);

        public void AdicionarSingle(Musica musica) => _singles.Add(musica);

        public void Validar()
        {
            ValidacaoDominio.NotNullOrEmpty(Nome, nameof(Nome));
        }

        protected Banda() { Nome = null!; }
    }
}
