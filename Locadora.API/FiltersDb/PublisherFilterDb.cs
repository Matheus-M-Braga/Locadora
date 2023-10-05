using Locadora.API.Repository.Pagination;

namespace Locadora.API.FiltersDb
{
    public class PublisherFilterDb : PagedBaseRequest
    {
        public string? FilterValue { get; set; }
    }
}
