using Locadora.API.Repository.Pagination;

namespace Locadora.API.FiltersDb
{
    public class UserFilterDb : PagedBaseRequest
    {
        public string? FilterValue { get; set; }
    }
}
