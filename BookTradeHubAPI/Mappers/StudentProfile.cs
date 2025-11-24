using AutoMapper;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using MongoDB.Bson;

namespace BookTradeHubAPI.Mappers;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentGetDto>()
            .ForMember(d => d.Books, opt => opt.Ignore());
        CreateMap<StudentCreateDto, Student>()
            .AfterMap((src, dest) => {
                dest.Id = ObjectId.GenerateNewId().ToString();
            });
    }
}
