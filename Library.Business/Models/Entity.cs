namespace Library.Business.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
