using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Usuario : RaizDeAgregacao
    {
        private readonly List<Playlist> _playlists = new();
        private readonly List<MusicaFavorita> _musicasFavoritas = new();

        public string Nome { get; private set; }

        public string Username { get; private set; }

        public string Senha { get; private set; }

        public Guid PlanoId { get; private set; }

        public IReadOnlyCollection<Playlist> Playlists => _playlists;

        public IReadOnlyCollection<MusicaFavorita> MusicasFavoritas => _musicasFavoritas;

        public DateTime DataCriada { get; private set; }

        public Usuario(string nome, string username, string senha, Guid planoId)
        {
            Nome = nome;
            Username = username;
            Senha = senha;
            PlanoId = planoId;
            DataCriada = DateTime.Now;
        }

        public void AlterarPlano(Guid novoPlanoId) => PlanoId = novoPlanoId;

        public void AdicionarPlaylist(Playlist playlist) => _playlists.Add(playlist);

        public void FavoritarMusica(Guid musicaId)
        {
            if (_musicasFavoritas.Any(f => f.MusicaId == musicaId)) return;
            _musicasFavoritas.Add(new MusicaFavorita(musicaId, DateTime.UtcNow));
        }

        public void DesfavoritarMusica(Guid musicaId)
        {
            var favorita = _musicasFavoritas.FirstOrDefault(f => f.MusicaId == musicaId);
            if (favorita is not null) _musicasFavoritas.Remove(favorita);
        }

        protected Usuario() { Nome = null!; Username = null!; Senha = null!; }
    }
}
