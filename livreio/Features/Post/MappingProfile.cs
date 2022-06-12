using AutoMapper;

namespace livreio.Features.Post;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
    }
    
}