using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Integrante : Entidade
    {
        public string Nome { get; private set; }

        public DateTime DataDeNascimento { get; private set; }

        public DateTime DataCriada { get; private set; }

        public Integrante(
            string nome,
            DateTime dataDeNascimento
        )
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            DataCriada = DateTime.Now;
        }

        public Integrante() { Nome = null!; }
    }
}
