using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Models;

namespace Locadora.API.Helpers {
    public class ProjectProfile : Profile {
        public ProjectProfile() {
            CreateMap<Users, UserRentalDto>().ReverseMap();
            CreateMap<Users, CreateUserDto>().ReverseMap();

            CreateMap<Books, BooksDto>().ReverseMap();
            CreateMap<Books, CreateBookDto>().ReverseMap();
            CreateMap<Books, BookRentalDto>().ReverseMap();
            CreateMap<Books, UpdateBookDto>().ReverseMap();
            CreateMap<BooksDto, UpdateBookDto>().ReverseMap();

            CreateMap<Publishers, PublisherBookDto>().ReverseMap();
            CreateMap<Publishers, CreatePublisherDto>().ReverseMap();

            CreateMap<Rentals, RentalsDto>().ReverseMap();  
            CreateMap<Rentals, UpdateRentalDto>().ReverseMap();
            CreateMap<Rentals, CreateRentalDto>().ReverseMap();
        }
    }
}
