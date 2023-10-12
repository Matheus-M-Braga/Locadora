#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace Locadora.API.Models {
    public class Rentals {
        public Rentals() { }
        public Rentals(int id, int bookId, int userId, DateTime rentalDate, DateTime forecastDate, DateTime? returnDate) {
            Id = id;
            BookId = bookId;
            UserId = userId;
            RentalDate = rentalDate;
            ForecastDate = forecastDate;
            ReturnDate = returnDate;
        }
        public int Id { get; set; }
        public int BookId { get; set; }
        public Books Book { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ForecastDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
