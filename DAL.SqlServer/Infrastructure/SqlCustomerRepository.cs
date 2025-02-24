using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;
using System.Runtime.InteropServices;

namespace DAL.SqlServer.Infrastructure;

public class SqlCustomerRepository : BaseSqlRepository, ICustomerRepository
{
    private readonly AppDbContext _context;

    public SqlCustomerRepository(string connectionString, AppDbContext context) : base(connectionString)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        var sql = @"INSERT INTO Customers([Name],[Surname],[Email],[CreatedBy])
                    VALUES(@Name,@Surname,@Email,@CreatedBy)";

        using var connection = OpenConnection();
        var generatedId = await connection.ExecuteScalarAsync(sql, customer);

    }

    public IQueryable<Customer> GetAll()
    {
        return _context.Customers.OrderByDescending(c => c.CreatedDate).Where(c => c.IsDeleted == false);
    }

    public async Task<Customer> GetByEmailAsync(string email)
    {
        var sql = @"SELECT *
                    FROM Customers AS c
                    WHERE c.Email = @email AND isDeleted = 0";

        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { email });
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var sql = @"SELECT *
                    FROM Customers AS c
                    WHERE c.Id = @id AND isDeleted = 0";

        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { id });
    }

    public async Task<IEnumerable<Customer>> GetByName(string name)
    {
        var sql = @"SELECT *
                    FROM Customers AS c
                    WHERE c.Name = @name AND isDeleted = 0";

        using var connection = OpenConnection();
        return await connection.QueryAsync<Customer>(sql, new { name });
    }

    public async Task<bool> Remove(int id, int deletedBy)
    {
        var checkSql = @"SELECT Id FROM Customers
                        WHERE Id=@id AND IsDeleted=0";

        var sql = @"UPDATE Customers
                    SET IsDeleted=1,
                    DeletedBy=@deletedBy,
                    DeletedDate=GETDATE()
                    Where Id=@id";

        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction();

        var customerId = await connection.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);

        if (!customerId.HasValue) return false;

        var affectedRow = await connection.ExecuteAsync(sql, new { id, deletedBy }, transaction);

        transaction.Commit();

        return affectedRow > 0;
    }

    public async Task Update(Customer customer)
    {
        var sql = @"UPDATE Customers
                    SET Name=@Name,
                    Surname=@Surname,
                    Email=@Email,
                    UpdatedBy=@UpdatedBy,
                    UpdatedDate=GETDATE()
                    WHERE Id=@id";

        using var connection = OpenConnection();
        await connection.QueryAsync<Product>(sql, customer);
    }
}
