namespace InfnetStreaming.Application.Excecoes
{
    public class NotFoundException : Domain.Excecoes.NotFoundException
    {
        public NotFoundException(string? message) : base(message) { }
    }
}
