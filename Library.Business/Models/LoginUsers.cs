namespace Library.Business.Models
{
    public class LoginUsers : Entity
    {
        public LoginUsers() { }

        public LoginUsers(int id, string name, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreateAt = DateTime.Now;
        }

        public LoginUsers(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
