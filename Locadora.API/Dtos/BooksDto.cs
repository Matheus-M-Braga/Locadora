﻿using Locadora.API.Models;

namespace Locadora.API.Dtos {
    public class BooksDto {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public int? PublisherId { get; set; }
        public string? Release { get; set; }
        public int? Quantity { get; set; }
    }
}
