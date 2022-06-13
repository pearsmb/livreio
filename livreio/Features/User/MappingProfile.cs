using AutoMapper;
using livreio.API;
using livreio.Features.Post;

namespace livreio.Features.User;

public class MappingProfile : Profile
{
    
    
    public MappingProfile()
    {
        CreateMap<AppUser, GetUserDto>();
        
    }
    
    
}