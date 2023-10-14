#pragma warning disable CS8618
using Locadora.API.Dtos.Book;
using Locadora.API.Dtos.User;

namespace Locadora.API.Dtos.Rental
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookRentalDto Book { get; set; }
        public int UserId { get; set; }
        public UserRentalDto User { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ForecastDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
