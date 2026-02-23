namespace khaoduan_api.Models;

public class News
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Publisher { get; set; }
    public string Status { get; set; } = "draft";
    public DateTime PublishedTime { get; set; }
    public DateTime LastEdittedTime { get; set; }
    public string[]? Keywords { get; set; } = [];
    public string[]? Tags { get; set; } = [];
    public int Share { get; set; } = 0;
}