using DAL.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.SqlServer;

public static class DependencyInjections
{
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<AppDbontext>(opt => opt.UseSqlServer(connectionString));  
        return services;
    }
}
