namespace Locadora.API.Dtos {
    public class BookRegisterDto {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public int? PublisherId { get; set; }
        public string? Release { get; set; }
        public int? Quantity { get; set; }
    }
}
