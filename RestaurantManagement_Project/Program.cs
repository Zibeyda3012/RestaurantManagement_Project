using Application;
using Application.Security;
using DAL.SqlServer;
using Microsoft.OpenApi.Models;
using RestaurantManagement.Middlewares;
using RestaurantManagement_Project.Infrastructure;
using RestaurantManagement_Project.Middlewares;
using RestaurantManagement_Project.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter valid token here ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            new List<string>()
          }
        });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserContext, HttpUserContext>();

builder.Services.AddAuthenticationService(builder.Configuration);


var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddApplicationServices();
builder.Services.AddSqlServerServices(conn);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();
app.UseMiddleware<CorsMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
