using Application.CQRS.Cars.DTOs;
using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using Application.CQRS.Products.Queries.Responses;
using Application.CQRS.Users.DTOs;
using AutoMapper;
using Domain.Entities;
using static Application.CQRS.Cars.Handlers.AddCar;
using static Application.CQRS.Users.Handlers.Register;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, Application.CQRS.Users.DTOs.GetByIdDTO>().ReverseMap();
        CreateMap<Command, User>().ReverseMap();
        CreateMap<User, RegisterDTO>().ReverseMap();

        CreateMap<User, UpdateDTO>().ReverseMap();

        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<CreateProductResponse, Product>().ReverseMap();

        CreateMap<Product, UpdateProductRequest>().ReverseMap();
        CreateMap<UpdateProductResponse, Product>().ReverseMap();

        CreateMap<GetAllProductResponse, Product>().ReverseMap();
        CreateMap<GetByIdProductResponse, Product>().ReverseMap();
        CreateMap<GetByNameProductResponse, Product>().ReverseMap();

        CreateMap<Car, AddCarCommandRequest>().ReverseMap();
        CreateMap<AddCarDTO,Car>().ReverseMap();    
        CreateMap<Application.CQRS.Cars.DTOs.GetByIdDTO, Car>().ReverseMap();   
    }
}
