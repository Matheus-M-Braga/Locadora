#pragma warning disable CS8618


namespace Locadora.API.Dtos {
    public class CreateUserDto {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class UserRentalDto {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
