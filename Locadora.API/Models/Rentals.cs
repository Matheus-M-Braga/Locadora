namespace Locadora.API.Models {
    public class Rentals {
        public Rentals() { }
        public Rentals(int id, int bookId, int userId, DateTime rentalDate, DateTime forecastDate, DateTime? returnDate, string status) {
            this.Id = id;
            this.BookId = bookId;
            this.UserId = userId;
            this.RentalDate = rentalDate;
            this.ForecastDate = forecastDate;
            this.ReturnDate = returnDate;
            this.Status = status;
        }
        public int? Id { get; set; }
        public int? BookId { get; set; }
        public Books? Book { get; set; }
        public int? UserId { get; set; }
        public Users? User { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Status { get; set; }
    }
}
