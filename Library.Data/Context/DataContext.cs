#pragma warning disable CS8618
using Library.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        public DbSet<LoginUsers> LoginUsers { get; set; }
    }
}
