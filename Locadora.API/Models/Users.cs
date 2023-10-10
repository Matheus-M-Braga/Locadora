#pragma warning disable CS8618


namespace Locadora.API.Models {
    public class Users {

        public Users() { }
        public Users(int id, string name, string city, string address, string email) {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            Email = email;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }
}
