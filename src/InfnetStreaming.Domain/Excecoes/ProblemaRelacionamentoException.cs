namespace InfnetStreaming.Domain.Excecoes
{
    public class ProblemaRelacionamentoException : Exception
    {
        public ProblemaRelacionamentoException(string? mensagem) : base(mensagem) { }
    }
}
