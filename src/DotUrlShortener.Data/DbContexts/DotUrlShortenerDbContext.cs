using DotUrlShortener.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotUrlShortener.Data.DbContexts;

public class DotUrlShortenerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
}
