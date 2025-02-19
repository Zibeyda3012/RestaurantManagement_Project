using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCategoryRepository : BaseSqlRepository, ICategoryRepository
{
    private readonly AppDbContext _context;
    public SqlCategoryRepository(string connectionString, AppDbContext context) : base(connectionString)
    {
        _context = context;
    }

    public async Task AddAsync(Category category)
    {
        var sql = @"INSERT INTO Categories([Name],[CreatedBy])
                    VALUES(@Name,@CreatedBy)";

        using var connection = OpenConnection();
        var generatedId = await connection.ExecuteScalarAsync<int>(sql, category);

    }

    public IQueryable<Category> GetAll()
    {
        return _context.Categories.OrderByDescending(c => c.CreatedDate).Where(c => c.IsDeleted == false);
    }

    public async Task<Category> GetByIdAsyns(int id)
    {
        var sql = @"SELECT c.*
                   FROM Categories AS c
                   WHERE c.Id = @id AND isDeleted = 0";

        using var connection = OpenConnection();

        return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { id });
    }

    public async Task<Category> GetByName(string name)
    {
        var sql = @"DECLARE @searchText nvarchar(max)
                    SET @searchText = '%' + @name + '%'
                    SELECT c.* FROM Categories AS c
                    WHERE c.[Name] LIKE @searchText AND c.isDeleted=0";

        using var connection = OpenConnection();
        return await connection.QueryFirstOrDefault<Category>(sql,new { name });

    }

    public async Task<bool> Remove(int id, int deletedBy)
    {
        var checkSql = @"SELECT Id from Categories
                        WHERE Id=@id AND IsDeleted=0";

        var sql = @"UPDATE Categories
                    SET IsDeleted=1,
                    DeletedBy=@deletedBy,
                    DEletedDate=GETDATE()
                    Where Id=@id";

        using var connection = OpenConnection();

        using var transaction = connection.BeginTransaction();

        var categoryId = await connection.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);

        if (!categoryId.HasValue)
            return false;

        var affectedRow = await connection.ExecuteAsync(sql, new { id, deletedBy }, transaction);

        transaction.Commit();

        return affectedRow > 0;
    }

    public async Task Update(Category category)
    {
        var sql = @"UPDATE Categories
                    SET Name=@Name,
                    UpdatedBy=@UpdatedBy,
                    UpdatedDate=GETDATE()
                    WHERE Id=@id";

        using var connection = OpenConnection();
        await connection.QueryAsync<Category>(sql, category);

    }
}
