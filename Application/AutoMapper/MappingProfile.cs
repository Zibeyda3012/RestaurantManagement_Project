using Application.CQRS.Users.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,GetByIdDTO>().ReverseMap();
    }
}
