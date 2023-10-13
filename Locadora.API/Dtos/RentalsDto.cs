#pragma warning disable CS8618
using System.Text.Json.Serialization;

namespace Locadora.API.Dtos
{
    public class RentalsDto
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

    public class CreateRentalDto
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ForecastDate { get; set; }
    }

    public class UpdateRentalDto
    {
        public int? Id { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class RentalDashDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookRentalDto Book { get; set; }
        public string Status { get; set; }
    }
}
