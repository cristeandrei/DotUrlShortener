using Microsoft.EntityFrameworkCore;

namespace ReduceUrl.Data.DbContexts;

public class ReduceUrlDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Entities.ReducedUrl> ReducedUrls { get; set; }
}
