using Microsoft.EntityFrameworkCore;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.Data.Models;
using ReduceUrl.Data.Repositories.Interfaces;

namespace ReduceUrl.Data.Repositories;

internal sealed class ReduceUrlRepository(ReduceUrlDbContext reduceUrlDbContext)
    : IReduceUrlRepository
{
    public async Task<IList<ReducedUrl>> GetAll(CancellationToken cancellationToken = default)
    {
        return await reduceUrlDbContext.ReducedUrls.ToListAsync(cancellationToken);
    }
}
