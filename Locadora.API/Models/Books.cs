#pragma warning disable CS8618


namespace Locadora.API.Models {
    public class Books {

        public Books() { }
        public Books(int id, string name, string author, int publisherId, string release, int quantity, int rented) {
            Id = id;
            Name = name;
            Author = author;
            PublisherId = publisherId;
            Release = release;
            Quantity = quantity;
            Rented = rented;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public Publishers Publisher { get; set; }
        public string Release { get; set; }
        public int Quantity { get; set; }
        public int Rented { get; set; }
    }
}
