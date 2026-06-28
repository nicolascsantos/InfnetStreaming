namespace InfnetStreaming.Application.Services
{
    public static class SenhaHelper
    {
        public static string Hash(string senha) =>
            InfnetStreaming.Domain.Services.SenhaHelper.Hash(senha);

        public static bool Verificar(string senha, string senhaArmazenada) =>
            InfnetStreaming.Domain.Services.SenhaHelper.Verificar(senha, senhaArmazenada);
    }
}
