using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlProductRepository : BaseSqlRepository, IProductRepository
{

    private readonly AppDbContext _context;

    public SqlProductRepository(string connectionString, AppDbContext context) : base(connectionString)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        var sql = @"INSERT INTO Products([Name],[Price],[StockQuantity],[CreatedBy])
                    VALUES(@Name,@Price,@StockQuantity,@CreatedBy)";

        using var connection = OpenConnection();
        var generatedId = await connection.ExecuteScalarAsync(sql, product);

    }

    public IQueryable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove(int id, int deletedBy)
    {
        throw new NotImplementedException();
    }

    public Task Update(Product product)
    {
        throw new NotImplementedException();
    }
}
