using InfnetStreaming.Domain.SeedWork;

namespace InfnetStreaming.Domain.Entities
{
    public class Plano : RaizDeAgregacao
    {
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Plano(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
            DataCriacao = DateTime.Now;
        }

        protected Plano() { Nome = null!; }
    }
}
