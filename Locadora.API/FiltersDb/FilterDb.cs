using Locadora.API.Repository.Pagination;

namespace Locadora.API.FiltersDb
{
    public class FilterDb : PagedBaseRequest
    {
        public string? FilterValue { get; set; }
    }
}