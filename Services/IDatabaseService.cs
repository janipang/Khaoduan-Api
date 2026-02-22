using khaoduan_api.Models;

namespace khaoduan_api.Services;

public interface IDatabaseService
{
    Task<List<News>> GetNewsAsync();
    Task<News?> GetNewsBYIdAsync(int id);
    Task<List<News>> AddNewsAsync(News news);
    Task<bool> UpdateNewsAsync(int id, News news);
    Task<bool> DeleteNewsAsync(int id);
}