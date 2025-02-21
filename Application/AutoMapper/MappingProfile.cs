using Application.CQRS.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using System.Data;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,GetByIdDTO>().ReverseMap();
        CreateMap<Command,User>().ReverseMap(); 
        CreateMap<User,RegisterDTO>().ReverseMap(); 
    }
}
