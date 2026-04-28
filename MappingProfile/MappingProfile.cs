using AutoMapper;
using BookApi.DTOs;
using BookApi.Models;

namespace BookApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book → BookDto (flatten many-to-many)
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Authors,
                opt => opt.MapFrom(src =>
                    src.BookAuthors.Select(ba => ba.Author)));

        CreateMap<Author, AuthorSummaryDto>();

        // Author → AuthorDto
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.BookTitles,
                opt => opt.MapFrom(src =>
                    src.BookAuthors.Select(ba => ba.Book.Title)));

        // DTOs → Models
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<CreateAuthorDto, Author>();
    }
}