using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Models;

namespace Locadora.API.Helpers {
    public class ProjectProfile : Profile {
        public ProjectProfile() {
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<Books, BooksDto>().ReverseMap();
            CreateMap<Publishers, PublishersDto>().ReverseMap();
            CreateMap<Rentals, RentalsDto>().ReverseMap();  
            CreateMap<Rentals, RentalReturnDto>().ReverseMap();
        }
    }
}
