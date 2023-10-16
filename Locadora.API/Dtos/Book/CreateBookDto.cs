﻿#pragma warning disable CS8618
namespace Locadora.API.Dtos.Book
{
    public class CreateBookDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int? PublisherId { get; set; }
        public int? Release { get; set; }
        public int? Quantity { get; set; }
    }
}
