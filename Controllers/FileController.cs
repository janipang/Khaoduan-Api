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

        using var stream = System.IO.File.Create(Path.Combine(uploadPath, fileName));
        await file.CopyToAsync(stream);

        return Ok(new { filename = fileName });
    }

    [AllowAnonymous]
    [HttpGet("{filename}")]
    public IActionResult GetFile(string filename)
    {
        var uploadPath = _config["Upload:Path"]!;
        var path = Path.Combine(uploadPath, filename);
        
        if (!System.IO.File.Exists(path)) return NotFound();

        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(path, out string contentType))
        {
            contentType = "application/octet-stream";
        }

        return PhysicalFile(path, contentType);
    }
}