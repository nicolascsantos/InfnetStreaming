using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class BandaFavorita : Entidade
    {
        public Guid BandaId { get; private set; }
        public DateTime DataFavoritado { get; private set; }

        public BandaFavorita(Guid bandaId)
        {
            BandaId = bandaId;
            DataFavoritado = DateTime.UtcNow;
        }

        protected BandaFavorita() { }
    }
}
