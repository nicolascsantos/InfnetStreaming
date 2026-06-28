using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Application.Services.Busca.BuscarMusicas
{
    public record BuscarMusicasInput(int Page, int PerPage, string Search, string OrderBy, OrdemBusca Order);
}
