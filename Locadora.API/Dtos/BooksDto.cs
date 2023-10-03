#pragma warning disable CS8618
namespace Locadora.API.Dtos
{
    public class BooksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public PublisherBookDto Publisher { get; set; }
        public string Release { get; set; }
        public int Quantity { get; set; }
        public int Rented { get; set; }
    }

    public class CreateBookDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public string Release { get; set; }
        public int Quantity { get; set; }
    }
    
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public string Release { get; set; }
        public int Quantity { get; set; }
    }

    public class BookRentalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
