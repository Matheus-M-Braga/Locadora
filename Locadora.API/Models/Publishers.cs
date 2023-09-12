namespace Locadora.API.Models {
    public class Publishers {
        public Publishers() { }
        public Publishers(int id, string name, string city) {
            this.Id = id;
            this.Name = name;
            this.City = city;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
      
    }
}
