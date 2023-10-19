#pragma warning disable CS8618


namespace Library.Business.Models
{
    public class Publishers
    {
        public Publishers() { }
        public Publishers(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

    }
}
