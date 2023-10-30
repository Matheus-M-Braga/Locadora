namespace Library.Business.Models.Dtos.Rental
{
    public class CreateRentalDto
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public DateTime? RentalDate { get; set; }
        public DateTime? ForecastDate { get; set; }
    }
}
