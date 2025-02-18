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
        return _context.Products.OrderByDescending(p => p.CreatedDate).Where(p => p.IsDeleted == false);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var sql = @"SELECT *
                    FROM Products AS p
                    WHERE p.Id = @id AND isDeleted = 0";

        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { id });
    }

    public async Task<IEnumerable<Product>> GetByName(string name)
    {
        var sql = @"DECLARE @SearchText nvarchar(max)
                    SET @SearchText = '%' + @name + '%'
                    SELECT * FROM Products AS p
                    WHERE p.[Name] LIKE @SearchText AND isDeleted=0";

        using var connection = OpenConnection();
        return await connection.QueryAsync<Product>(sql, name);
    }

    public async Task<bool> Remove(int id, int deletedBy)
    {
        var checkSql = @"SELECT Id FROM Products
                        WHERE Id=@id AND IsDeleted=0";

        var sql = @"UPDATE Products
                    SET IsDeleted=1,
                    DeletedBy=@deletedBy,
                    DEletedDate=GETDATE()
                    Where Id=@id";

        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction();

        var productId = await connection.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);

        if (!productId.HasValue) return false;

        var affectedRow = await connection.ExecuteAsync(sql, new { id, deletedBy }, transaction);

        transaction.Commit();

        return affectedRow > 0;

    }

    public async Task Update(Product product)
    {
        var sql = @"UPDATE Products
                    SET Name=@Name,
                    UpdatedBy=@UpdatedBy,
                    UpdatedDate=GETDATE()
                    WHERE Id=@id";

        using var connection = OpenConnection();
        await connection.QueryAsync<Product>(sql, product);
    }
}
