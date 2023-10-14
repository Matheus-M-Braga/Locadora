using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Book;
using Locadora.API.Dtos.Publisher;
using Locadora.API.Dtos.Rental;
using Locadora.API.Dtos.User;
using Locadora.API.Models;

namespace Locadora.API.Mapper
{
    public class ProjectProfile : Profile {
        public ProjectProfile() {
            CreateMap<Users, UserRentalDto>().ReverseMap();
            CreateMap<Users, CreateUserDto>().ReverseMap();
            CreateMap<Users, UpdateUserDto>().ReverseMap();

            CreateMap<Books, BookDto>().ReverseMap();
            CreateMap<Books, CreateBookDto>().ReverseMap();
            CreateMap<Books, BookRentalDto>().ReverseMap();
            CreateMap<Books, UpdateBookDto>().ReverseMap();
            CreateMap<Books, BookDashDto>().ReverseMap();

            CreateMap<Publishers, PublisherBookDto>().ReverseMap();
            CreateMap<Publishers, CreatePublisherDto>().ReverseMap();
            CreateMap<Publishers, UpdatePublisherDto>().ReverseMap();
            CreateMap<List<Publishers>, PagedBaseResponseDto<Publishers>>().ReverseMap();

            CreateMap<Rentals, RentalDto>().ReverseMap();
            CreateMap<Rentals, UpdateRentalDto>().ReverseMap();
            CreateMap<Rentals, CreateRentalDto>().ReverseMap();
            CreateMap<Rentals, RentalDashDto>().ReverseMap();
        }
    }
}
