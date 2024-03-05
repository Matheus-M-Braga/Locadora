#pragma warning disable CS8618
namespace Library.Business.Models
{
    public class Publishers : Entity
    {
        public Publishers() { }

        public Publishers(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
            CreateAt = DateTime.Now;
        }

        public string Name { get; set; }
        public string City { get; set; }

    }
}
