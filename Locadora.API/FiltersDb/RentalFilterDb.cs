using Locadora.API.Repository.Pagination;

namespace Locadora.API.FiltersDb
{
    public class RentalFilterDb : PagedBaseRequest
    {
        public string Name { get; set; } = "";
    }
}
