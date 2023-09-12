using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Models;

namespace Locadora.API.Helpers {
    public class ProjectProfile : Profile {
        public ProjectProfile() {
            CreateMap<Users, UsersDto>();
        }
    }
}
