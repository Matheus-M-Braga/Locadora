namespace Locadora.API.Dtos
{
    public class PagedBaseResponseDto<T>
    {
        public PagedBaseResponseDto(int totalRegisters, int totalPages, List<T> data)
        {
            TotalRegisters = totalRegisters;
            TotalPages = totalPages;
            Data = data;
        }

        public int TotalRegisters { get; private set; }
        public int TotalPages { get; private set; }
        public List<T> Data { get; private set; }
    }
}
