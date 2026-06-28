namespace InfnetStreaming.Domain.Excecoes
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base(message) { }
    }
}
