#pragma warning disable CS8618
using Library.Business.Models.Dtos.Book;

namespace Library.Business.Models.Dtos.Rental
{
    public class RentalCountDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookListDto Book { get; set; }
        public string Status { get; set; }
    }
}
