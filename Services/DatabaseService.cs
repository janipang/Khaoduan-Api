using System.Data;
using khaoduan_api.Data;
using khaoduan_api.Models;
using Microsoft.EntityFrameworkCore;

namespace khaoduan_api.Services;

public class DatabaseService : IDatabaseService
{
    private readonly AppDbContext _context;
    public DatabaseService(AppDbContext context)
    {
        _context = context;
    }


    public Task<List<News>> AddNewsAsync(News news)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteNewsAsync(int id)
    {
        var rows = await _context.Database
            .ExecuteSqlRawAsync("DELETE FROM News WHERE id = {0}", id);
        return rows > 0;
    }

    public async Task<List<News>> GetNewsAsync()
      => await _context.News
        .FromSqlRaw("SELECT * FROM News WHERE 1 = 1")
        .ToListAsync();

    public async Task<News?> GetNewsBYIdAsync(int id)
        => await _context.News
        .FromSqlRaw("SELECT * FROM News WHERE Id = {0}", id)
        .FirstOrDefaultAsync();

    public Task<bool> UpdateNewsAsync(int id, News news)
    {
        throw new NotImplementedException();
    }
}