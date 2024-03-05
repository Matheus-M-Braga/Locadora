namespace Library.Business.Models
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public LoginUser(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        public LoginUser(string name, string email)
        {
            Name = name;
            Email = email;
        }
        public void ChangePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
