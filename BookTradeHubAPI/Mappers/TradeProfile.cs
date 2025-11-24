using AutoMapper;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using MongoDB.Bson;

namespace BookTradeHubAPI.Mappers;

public class TradeProfile : Profile
{
    public TradeProfile()
    {
        CreateMap<Trade, TradeGetDto>()
            .ForMember(d => d.Student1, opt => opt.Ignore())
            .ForMember(d => d.Student2, opt => opt.Ignore())
            .ForMember(d => d.newStudent1Books, opt => opt.MapFrom(s => new List<BookGetDto>()))
            .ForMember(d => d.newStudent2Books, opt => opt.MapFrom(s => new List<BookGetDto>()));
        CreateMap<TradeCreateDto, Trade>()
            .AfterMap((src, dest) => {
                dest.Id = ObjectId.GenerateNewId().ToString();
                dest.Date = DateTime.Now;
            });
    }
}
