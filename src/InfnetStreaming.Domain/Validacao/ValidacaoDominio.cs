using InfnetStreaming.Domain.Excecoes;

namespace InfnetStreaming.Domain.Validacao
{
    public static class ValidacaoDominio
    {
        public static void NotNull(object? alvo, string nomeCampo)
        {
            if (alvo is null)
                throw new ValidacaoDominioException($"Campo {nomeCampo} não pode ser nulo.");
        }

        public static void NotNullOrEmpty(string? alvo, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(alvo))
                throw new ValidacaoDominioException($"{nomeCampo} should not be empty or null.");
        }

        public static void MinLength(string alvo, int tamanhoMinimo, string nomeCampo)
        {
            if (alvo.Length < tamanhoMinimo)
                throw new ValidacaoDominioException($"{nomeCampo} deve ter pelo menos {tamanhoMinimo} caracteres.");
        }

        public static void MaxLength(string alvo, int tamanhoMaximo, string nomeCampo)
        {
            if (alvo.Length > tamanhoMaximo)
                throw new ValidacaoDominioException($"{nomeCampo} deve ter no máximo {tamanhoMaximo} caracteres.");
        }
    }
}
