using System.Net.Sockets;
using khaoduan_api.Models;
using khaoduan_api.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

namespace khaoduan_api.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController(IDatabaseService db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<News>>> GetNews()
        => await db.GetNewsAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<News>> GetNewsById(int id)
    {
        var news = await db.GetNewsBYIdAsync(id);
        if (news is null)
        {
            return NotFound("News with id = '' not found");
        }
        return Ok(news);
    }

    [HttpPost]
    public async Task<ActionResult<News>> PostNews(News news)
    {
        var newsWithId = await db.AddNewsAsync(news);
        return Ok(newsWithId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<News>> PutNews(int id, News news)
    {
        var newsWithId = await db.UpdateNewsAsync(id, news);
        return Ok(newsWithId);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<News>> DeleteNews(int id)
    {
        var news = await db.DeleteNewsAsync(id);
        if (news is false)
        {
            return NotFound("News with id = '' not found");
        }
        return Ok(news);
    }
}
