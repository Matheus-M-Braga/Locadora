﻿#pragma warning disable CS8618
namespace Locadora.API.Dtos.User
{
    public class UpdateUserDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}