using Application.AutoMapper;
using Application.PipelineBehaviours;
using Application.Services.BackgroundServices;
using Application.Services.LogService;
using Application.StrategyPattern;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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

        #region Serilog

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File($@"log\appLog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddScoped<ILoggerService,LoggerService>(); 

        #endregion


        services.AddScoped<ICarStrategy,CarStrategy>();    
        services.AddScoped<ICarContext,CarContext>();    


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());   
        
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationPipelineBehavior<,>));

        services.AddHostedService<DeleteUserBackgroundService>();

        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
