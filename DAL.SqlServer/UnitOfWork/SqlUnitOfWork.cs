using DAL.SqlServer.Context;
using DAL.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbontext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbontext _context = context;

    public SqlCategoryRepository _sqlCategoryRepository;

    public ICategoryRepository CategoryRepository => _sqlCategoryRepository ?? new SqlCategoryRepository(_connectionString, _context);

    public async Task<int> SaveChanges() => await _context.SaveChangesAsync();
 
}
