#pragma warning disable CS8618
using Locadora.API.Dtos.Book;

namespace Locadora.API.Dtos.Rental
{
    public class RentalDashDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookRentalDto Book { get; set; }
        public string Status { get; set; }
    }
}
