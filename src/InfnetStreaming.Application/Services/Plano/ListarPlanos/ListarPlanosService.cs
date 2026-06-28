using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Plano.ListarPlanos
{
    public class ListarPlanosService
    {
        private readonly IRepositorioPlano _repositorioPlano;

        public ListarPlanosService(IRepositorioPlano repositorioPlano) => _repositorioPlano = repositorioPlano;

        public async Task<IReadOnlyList<ListarPlanosOutput>> Executar(CancellationToken cancellationToken)
        {
            var planos = await _repositorioPlano.ListarTodos(cancellationToken);
            return planos.Select(p => new ListarPlanosOutput(p.Id, p.Nome, p.Valor)).ToList();
        }
    }
}
