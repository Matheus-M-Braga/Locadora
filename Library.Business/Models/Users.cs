#pragma warning disable CS8618
namespace Library.Business.Models
{
    public class Users : Entity
    {
        public Users() { }

        public Users(int id, string name, string city, string address, string email)
        {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            Email = email;
            CreateAt = DateTime.Now;
        }

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
