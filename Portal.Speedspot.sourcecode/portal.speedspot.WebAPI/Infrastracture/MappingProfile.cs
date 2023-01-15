using AutoMapper;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.ViewModels;

namespace portal.speedspot.WebAPI.Infrastracture
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleViewModel, ApplicationRole>().ReverseMap();
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<CreateUserViewModel, ApplicationUser>()
                .ForMember(a => a.UserName, c => c.MapFrom(b => b.Email));
        }
    }
}
