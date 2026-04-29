using AutoMapper;
using BookApi.DTOs;
using BookApi.Models;

namespace BookApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book → BookDto (include publisher + flatten many-to-many)
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Authors,
            opt => opt.MapFrom(src =>
            src.BookAuthors.Select(ba => ba.Author)))
            .ForMember(dest => dest.Publisher,
            opt => opt.MapFrom(src => src.Publisher));

        CreateMap<Author, AuthorSummaryDto>();

        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.BookTitles,
            opt => opt.MapFrom(src =>
            src.BookAuthors.Select(ba => ba.Book.Title)));

        // Publisher mappings
        CreateMap<Publisher, PublisherDto>()
            .ForMember(dest => dest.BookTitles,
            opt => opt.MapFrom(src =>
            src.Books.Select(b => b.Title)));

        CreateMap<Publisher, PublisherSummaryDto>();
        CreateMap<CreatePublisherDto, Publisher>();

        // DTOs → Models
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<CreateAuthorDto, Author>();
    }
}