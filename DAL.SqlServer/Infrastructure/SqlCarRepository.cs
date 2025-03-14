using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCarRepository(AppDbContext context) : ICarRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Car car)
    {
        _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();  

    }

    public async Task<IQueryable<Car>> GetAll()
    {
        return _context.Cars.OrderByDescending(c => c.CreatedDate).Where(c => c.IsDeleted == false);
    }

    public Task<Car> GetByIdAsync(int id)
    {
        return _context.Cars.Where(c => c.IsDeleted == false).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Remove(int id)
    {
        var currentCar = _context.Cars.FirstOrDefault(c => c.Id == id);
        currentCar.IsDeleted = true;
        currentCar.DeletedBy = 1;
        currentCar.DeletedDate = DateTime.Now;
    }
}
