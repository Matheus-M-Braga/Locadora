using Locadora.API.Repository.Pagination;

namespace Locadora.API.FiltersDb
{
    public class BookFilterDb : PagedBaseRequest
    {
        public string? FilterValue { get; set; }
    }
}