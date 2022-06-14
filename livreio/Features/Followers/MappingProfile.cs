using AutoMapper;
using livreio.API;

namespace livreio.Features.Followers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<AppUser, FollowDto>();

    }
}