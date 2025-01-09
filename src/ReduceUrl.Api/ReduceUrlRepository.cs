using Microsoft.EntityFrameworkCore;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.Data.Models;

namespace ReduceUrl.Api;

internal sealed class ReduceUrlRepository(ReduceUrlDbContext reduceUrlDbContext)
    : IReduceUrlRepository
{
    public async Task<IList<ReducedUrl>> GetAll(CancellationToken cancellationToken = default)
    {
        return await reduceUrlDbContext.ReducedUrls.ToListAsync(cancellationToken);
    }
}

internal interface IReduceUrlRepository
{
    Task<IList<ReducedUrl>> GetAll(CancellationToken cancellationToken = default);
}
