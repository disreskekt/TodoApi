using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Domain.Models;

namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDto>()
            .ForMember(dest => dest.Hash, options => options.MapFrom(src => HashHelper.ComputeMd5Hash(src.Header)));
        
        CreateMap<Comment, CommentDto>();
    }
}