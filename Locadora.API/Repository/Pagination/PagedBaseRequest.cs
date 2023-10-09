namespace Locadora.API.Repository.Pagination
{
    public class PagedBaseRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }

        public PagedBaseRequest()
        {
            Page = 1;
            PageSize = 5;
            OrderBy = "Id";
        }
    }
    public class FilterDb : PagedBaseRequest
    {
        public string? FilterValue { get; set; }
    }
}
