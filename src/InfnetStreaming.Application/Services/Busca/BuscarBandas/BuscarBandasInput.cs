using InfnetStreaming.Domain.SeedWork.RepositoryBusca;

namespace InfnetStreaming.Application.Services.Busca.BuscarBandas
{
    public record BuscarBandasInput(int Page, int PerPage, string Search, string OrderBy, OrdemBusca Order);
}
