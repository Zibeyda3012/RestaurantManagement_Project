using Domain.Entities;

namespace Repository.Repositories;

public interface ICarRepository
{
    Task AddAsync(Car car);
    Task Remove(int id);
    Task<Car> GetByIdAsync(int id);
    Task<IQueryable<Car>> GetAll();

}
