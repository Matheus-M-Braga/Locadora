﻿namespace Library.Business.Pagination
{
    public class PagedBaseResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int TotalRegisters { get; set; }
        public int PageNumber { get; set; }
    }
}
