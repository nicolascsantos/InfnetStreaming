namespace InfnetStreaming.Domain.SeedWork.RepositoryBusca
{
    public class InputBusca
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public OrdemBusca Order { get; set; }
        public InputBusca(int page, int perPage, string search, string orderBy, OrdemBusca searchOrder)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            OrderBy = orderBy;
            Order = searchOrder;
        }
    }
}
