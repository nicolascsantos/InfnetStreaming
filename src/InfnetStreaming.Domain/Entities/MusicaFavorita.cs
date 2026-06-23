using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class MusicaFavorita : Entidade
    {
        public Guid MusicaId { get; private set; }

        public DateTime DataFavoritado { get; private set; }

        public MusicaFavorita(Guid musicaId, DateTime dataFavoritado)
        {
            MusicaId = musicaId;
            DataFavoritado = dataFavoritado;
        }

        protected MusicaFavorita() { }
    }
}
