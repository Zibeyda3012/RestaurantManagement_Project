using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Context;

public class AppDbontext : DbContext
{
    public AppDbontext(DbContextOptions<AppDbontext> options) : base(options)   
    {
        
    }

    public DbSet<Category> Categories { get; set; }
}
