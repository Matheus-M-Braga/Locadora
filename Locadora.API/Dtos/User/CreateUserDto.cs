﻿#pragma warning disable CS8618
namespace Locadora.API.Dtos.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
