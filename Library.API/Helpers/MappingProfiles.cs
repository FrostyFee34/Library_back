using AutoMapper;
using Library.API.DTOs;
using Library.Core.Entities;

namespace Library.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Book, BookOverviewDTO>()
            .ForMember(d => d.ReviewsNumber, o => o.MapFrom(s => s.Reviews.Count))
            .ForMember(d => d.Rating, o => o.MapFrom(s => s.Ratings
                .Select(r => r.Score).DefaultIfEmpty(0).Average()));
        CreateMap<Book, BookDetailsDTO>().ForMember(d => d.Rating, o => o.MapFrom(s => s.Ratings
            .Select(r => r.Score).DefaultIfEmpty(0).Average()));
        CreateMap<Review, ReviewDTO>();
        CreateMap<Book, BookToInsertDTO>().ReverseMap();
        CreateMap<Book, OnlyIdResponseDTO>();
        CreateMap<Review, OnlyIdResponseDTO>();
        CreateMap<Rating, OnlyIdResponseDTO>();
        CreateMap<ReviewToInsertDTO, Review>();
        CreateMap<RatingToInsertDTO, Rating>();
    }
}