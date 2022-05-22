using AutoMapper;
using Google.Apis.Books.v1.Data;

namespace livreio.Features.Books;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Volume.VolumeInfoData, BookDto>();
    }
    
}