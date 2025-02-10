using Domain.Entities;

namespace Repository.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task Update(Category category);
    Task<bool> Remove(int id, int deletedBy);
    IQueryable<Category> GetAll();
    Task<Category> GetByIdAsyns(int id);
    Task<IEnumerable<Category>> GetByName(string name); 
}
