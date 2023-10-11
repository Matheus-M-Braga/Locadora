#pragma warning disable CS8618

namespace Locadora.API.Dtos
{
    public class PublishersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class CreatePublisherDto
    {
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class UpdatePublisherDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class PublisherBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
