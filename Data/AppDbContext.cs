using khaoduan_api.Models;
using Microsoft.EntityFrameworkCore;

namespace khaoduan_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<News> News => Set<News>();
}