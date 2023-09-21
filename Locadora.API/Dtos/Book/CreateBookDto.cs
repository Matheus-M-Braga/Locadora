using System.ComponentModel.DataAnnotations;

namespace Locadora.API.Dtos
{
    public class CreateBookDto
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public int PublisherId { get; set; }
        public string? Release { get; set; }
        public int? Quantity { get; set; }
    }
}
