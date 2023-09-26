using Locadora.API.Helpers;

namespace Locadora.API.Models
{
    public class Rentals
    {
        public Rentals() { }
        public Rentals(int id, int bookId, int userId, string rentalDate, string forecastDate, string? returnDate)
        {
            this.Id = id;
            this.BookId = bookId;
            this.UserId = userId;
            this.RentalDate = rentalDate;
            this.ForecastDate = forecastDate;
            this.ReturnDate = returnDate;
        }
        public int Id { get; set; }
        public int BookId { get; set; }
        public Books? Book { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }
        public string? RentalDate { get; set; }
        public string? ForecastDate { get; set; }
        public string? ReturnDate { get; set; }
        public string? Status { get; set; }
    }
}
