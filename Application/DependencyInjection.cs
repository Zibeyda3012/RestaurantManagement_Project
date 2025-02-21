using Application.AutoMapper;
using Application.PipelineBehaviours;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());   
        
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationPipelineBehavior<,>));  


        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
