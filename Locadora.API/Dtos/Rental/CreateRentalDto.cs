using Locadora.API.Models;

namespace Locadora.API.Dtos
{
    public class CreateRentalDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string? RentalDate { get; set; }
        public string? ForecastDate { get; set; }
    }
}
