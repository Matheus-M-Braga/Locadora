﻿#pragma warning disable CS8618
namespace Locadora.API.Dtos
{
    public class RentalsDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookRentalDto Book { get; set; }
        public int UserId { get; set; }
        public UserRentalDto User { get; set; }
        public string RentalDate { get; set; }
        public string ForecastDate { get; set; }
        public string? ReturnDate { get; set; }
        public string Status { get; set; }
    }

    public class CreateRentalDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string RentalDate { get; set; }
        public string ForecastDate { get; set; }
    }

    public class UpdateRentalDto
    {
        public int Id { get; set; }
        public string ReturnDate { get; set; }
    }
}