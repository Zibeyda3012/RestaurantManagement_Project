using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshToken { get; set; }  

    public DbSet<Car> Cars { get; set; }    


}
