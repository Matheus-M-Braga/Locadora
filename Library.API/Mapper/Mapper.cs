using AutoMapper;
using Library.Business.Models;
using Library.Business.Models.Dtos.Book;
using Library.Business.Models.Dtos.LoginUser;
using Library.Business.Models.Dtos.Publisher;
using Library.Business.Models.Dtos.Rental;
using Library.Business.Models.Dtos.User;

namespace Library.Api.Mapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, UserListDto>().ReverseMap();
            CreateMap<Users, CreateUserDto>().ReverseMap();
            CreateMap<Users, UpdateUserDto>().ReverseMap();

            CreateMap<Books, BookDto>().ReverseMap();
            CreateMap<Books, CreateBookDto>().ReverseMap();
            CreateMap<Books, BookListDto>().ReverseMap();
            CreateMap<Books, UpdateBookDto>().ReverseMap();
            CreateMap<Books, BookCountDto>().ReverseMap();
            CreateMap<Books, BookRentedDto>().ReverseMap();

            CreateMap<Publishers, PublisherDto>().ReverseMap();
            CreateMap<Publishers, PublisherListDto>().ReverseMap();
            CreateMap<Publishers, CreatePublisherDto>().ReverseMap();
            CreateMap<Publishers, UpdatePublisherDto>().ReverseMap();

            CreateMap<Rentals, RentalDto>().ReverseMap();
            CreateMap<Rentals, UpdateRentalDto>().ReverseMap();
            CreateMap<Rentals, CreateRentalDto>().ReverseMap();
            CreateMap<Rentals, RentalCountDto>().ReverseMap();

            CreateMap<LoginUsers, LoginUserDto>().ReverseMap();
            CreateMap<LoginUserCreateDto, LoginUsers>();
            CreateMap<LoginUsers, LoginUserUpdateDto>().ReverseMap();
        }
    }
}
