﻿#pragma warning disable CS8618
namespace Library.Business.Models
{
    public class Books : Entity
    {
        public Books() { }

        public Books(int id, string name, string author, int publisherId, int release, int quantity, int rented)
        {
            Id = id;
            Name = name;
            Author = author;
            PublisherId = publisherId;
            Release = release;
            Quantity = quantity;
            Rented = rented;
            CreateAt = DateTime.Now;
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public Publishers Publisher { get; set; }
        public int Release { get; set; }
        public int Quantity { get; set; }
        public int Rented { get; set; }
    }
}
