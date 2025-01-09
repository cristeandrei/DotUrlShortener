using Microsoft.EntityFrameworkCore;
using ReduceUrl.Data.Entities;

namespace ReduceUrl.Data.DbContexts;

public class ReduceUrlDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ReducedUrl> ReducedUrls { get; set; }
}
