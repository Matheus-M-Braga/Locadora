using Locadora.API.Repository.Pagination;

namespace Locadora.API.FiltersDb
{
    public class RentalFilterDb : PagedBaseRequest
    {
        public string? FilterValue { get; set; }
    }
}
