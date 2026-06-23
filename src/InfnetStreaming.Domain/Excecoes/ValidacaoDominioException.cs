namespace InfnetStreaming.Domain.Excecoes
{
    public class ValidacaoDominioException : Exception
    {
        public string? Mensagem { get; set; }

        public ValidacaoDominioException(string? mensagem) : base(mensagem)
        {
            
        }
    }
}
