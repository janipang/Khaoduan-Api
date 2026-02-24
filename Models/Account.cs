using System.ComponentModel.DataAnnotations;

namespace khaoduan_api.Models;

public class Account
{
    [Key]
    public required string Username { get; set; }
    public required string Status { get; set; }
    public required string Password { get; set; }
}