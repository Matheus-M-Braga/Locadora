using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Business.Models.Dtos.LoginUser
{
    public class LoginUserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}
