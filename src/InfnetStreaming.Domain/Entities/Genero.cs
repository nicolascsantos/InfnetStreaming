using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Genero : Entidade
    {
        public string Nome { get; private set; }

        public DateTime DataCriada { get; private set; }

        public Genero(string nome, DateTime dataCriada)
        {
            Nome = nome;
            DataCriada = DateTime.Now;
        }
    }
}
