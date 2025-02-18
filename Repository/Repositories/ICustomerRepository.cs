using Domain.Entities;

namespace Repository.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task Update(Customer customer);
    Task<bool> Remove(int id);
    IQueryable<Customer> GetAll();
    Task<Customer> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetByName(string name);
}
