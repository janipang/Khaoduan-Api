using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace khaoduan_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IConfiguration _config;

    public FileController(IConfiguration config)
    {
        _config = config;
    }

    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file.Length == 0) return BadRequest("No file");

        var uploadPath = _config["Upload:Path"]!;
        Directory.CreateDirectory(uploadPath);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(uploadPath, fileName);

        using var stream = System.IO.File.Create(fullPath);
        await file.CopyToAsync(stream);

        return Ok(new { path = fullPath });
    }
}