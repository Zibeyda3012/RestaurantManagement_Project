using Domain.Entities;

namespace Repository.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task Update(Product product);
    Task<bool> Remove(int id,int deletedBy);
    IQueryable<Product> GetAll();
    Task<Category> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetByName(string name);
}
