using API_identity.DataTransferObjects;
using AutoMapper;

namespace API_identity.Entities.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserRegistertrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
