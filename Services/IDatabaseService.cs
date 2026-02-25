using System.Security.Principal;
using khaoduan_api.Models;

namespace khaoduan_api.Services;

public interface IDatabaseService
{
    Task<List<News>> GetNewsAsync(string? publisher = null, string[]? tags = null, string[]? keywords = null);
    Task<News?> GetNewsBYIdAsync(int id);
    Task<News> AddNewsAsync(News news);
    Task<bool> UpdateNewsAsync(int id, News news);
    Task<bool> DeleteNewsAsync(int id);
    Task<Account?> GetAccountAsync(string username); 
    Task<Account?> CreateAccountAsync(Account account);
}