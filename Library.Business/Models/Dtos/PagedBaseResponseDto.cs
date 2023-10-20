namespace Library.Business.Models.Dtos
{
    public class PagedBaseResponseDto<T>
    {
        public PagedBaseResponseDto(int totalRegisters, int totalPages, int page, List<T> data)
        {
            TotalRegisters = totalRegisters;
            TotalPages = totalPages;
            Page = page;
            Data = data;
        }

        public int TotalRegisters { get; private set; }
        public int TotalPages { get; private set; }
        public int Page { get; private set; }   
        public List<T> Data { get; private set; }
    }
}
