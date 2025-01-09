using ReduceUrl.Data.Models;

namespace ReduceUrl.Data.Repositories.Interfaces;

public interface IReduceUrlRepository
{
    Task<IList<ReducedUrl>> GetAll(CancellationToken cancellationToken = default);
}
