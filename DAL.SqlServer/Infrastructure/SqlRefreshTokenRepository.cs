using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public SqlRefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<RefreshToken> GetStoredRefreshToken(string refreshToken)
    {
        return _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }
}
