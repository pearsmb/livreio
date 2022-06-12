using AutoMapper;
using Google.Apis.Books.v1.Data;
using livreio.Domain;

namespace livreio.Features.Books;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<BookDto, Book>();
        CreateMap<Book, BookDto>();
        
        CreateMap<Volume, BookDto>()
            .ForMember(
                dest => dest.ImageLink,
                opt => opt.MapFrom(src => src.VolumeInfo.ImageLinks.Thumbnail))
            .ForMember(dest => dest.Authors,
                opt => opt.MapFrom(src => string.Join(",", src.VolumeInfo.Authors)))
            .ForMember(
                dest => dest.VolumeId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Title,
                opt => opt.MapFrom(src => src.VolumeInfo.Title))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.VolumeInfo.Description))
            .ForMember(
                dest => dest.AverageRating,
                opt => opt.MapFrom(src => src.VolumeInfo.AverageRating));
        
        CreateMap<Volume, Book>()
            .ForMember(dest => dest.Authors,
                opt => opt.MapFrom(src => string.Join(",", src.VolumeInfo.Authors)))
            .ForMember(
                dest => dest.VolumeId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Title,
                opt => opt.MapFrom(src => src.VolumeInfo.Title))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.VolumeInfo.Description))
            .ForMember(
                dest => dest.AverageRating,
                opt => opt.MapFrom(src => src.VolumeInfo.AverageRating));
        
        
            
        
        
    }
    
}