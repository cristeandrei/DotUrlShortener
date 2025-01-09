using Microsoft.EntityFrameworkCore;
using ReduceUrl.Data.Models;

namespace ReduceUrl.Data.DbContexts;

internal class ReduceUrlDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ReducedUrl> ReducedUrls { get; set; }
}
