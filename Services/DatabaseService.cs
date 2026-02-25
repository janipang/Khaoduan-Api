using System.Data;
using System.Text.Json;
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


    public async Task<News> AddNewsAsync(News news)
    {
        _context.News.Add(news);
        await _context.SaveChangesAsync();
        return news;
    }

    public async Task<bool> DeleteNewsAsync(int id)
    {
        var rows = await _context.Database
            .ExecuteSqlRawAsync("DELETE FROM News WHERE id = {0}", id);
        return rows > 0;
    }

    public async Task<List<News>> GetNewsAsync(string? publisher = null, string[]? tags = null, string[]? keywords = null)
    {
        var query = _context.News.AsQueryable();

        if (!string.IsNullOrWhiteSpace(publisher))
        {
            query = query.Where(n => n.Publisher == publisher);
        }

        var result = await query.ToListAsync();

        if (tags != null && tags.Length > 0)
        {
            result = result.Where(n => n.Tags != null && n.Tags.Any(t => tags.Contains(t))).ToList();
        }

        if (keywords != null && keywords.Length > 0)
        {
            result = result.Where(n => n.Keywords != null && n.Keywords.Any(k => keywords.Contains(k))).ToList();
        }

        return result;
    }

    public async Task<News?> GetNewsBYIdAsync(int id)
        => await _context.News
        .FromSqlRaw("SELECT * FROM News WHERE Id = {0}", id)
        .FirstOrDefaultAsync();

    public async Task<bool> UpdateNewsAsync(int id, News news)
    {
        var sth = await _context.Database
        .ExecuteSqlRawAsync(
            "UPDATE News SET Title = {1}, Content = {2}, Publisher = {3}, Status = {4}, PublishedTime = {5}, LastEdittedTime = {6}, Keywords = {7}, Tags = {8}, Share = {9} WHERE id = {0}", id, news.Title, news.Content, news.Publisher, news.Status, news.PublishedTime, news.LastEdittedTime, JsonSerializer.Serialize(news.Keywords), JsonSerializer.Serialize(news.Tags), news.Share);
        return sth > 0;
    }

    public async Task<Account?> GetAccountAsync(string username)
        => await _context.Accounts
        .FromSqlRaw("SELECT * FROM Accounts WHERE username = {0}", username)
        .FirstOrDefaultAsync();

    public async Task<Account?> CreateAccountAsync(Account account)
    {
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }
}