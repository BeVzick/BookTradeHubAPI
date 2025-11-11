using AutoMapper;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using MongoDB.Bson;

namespace BookTradeHubAPI.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookGetDto>();
        CreateMap<BookCreateDto, Book>()
            .AfterMap((src, dest) => {
                dest.Id = ObjectId.GenerateNewId().ToString();
            });
    }
}
