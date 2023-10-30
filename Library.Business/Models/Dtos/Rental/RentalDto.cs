#pragma warning disable CS8618
using Library.Business.Models.Dtos.Book;
using Library.Business.Models.Dtos.User;

namespace Library.Business.Models.Dtos.Rental
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookRentalDto Book { get; set; }
        public int UserId { get; set; }
        public UserRentalDto User { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
