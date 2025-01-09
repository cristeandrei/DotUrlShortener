using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.Data.Repositories;
using ReduceUrl.Data.Repositories.Interfaces;

namespace ReduceUrl.Data;

public static class DataExtensions
{
    public static void AddReduceUrlDataAccessLayer(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddNpgsqlDataSource(connectionName: "postgresdb");

        builder.Services.AddDbContextPool<ReduceUrlDbContext>(
            (s, o) =>
            {
                var npgsqlDataSource = s.GetRequiredService<NpgsqlDataSource>();

                o.UseNpgsql(npgsqlDataSource);
            }
        );

        builder.Services.AddScoped<IReduceUrlRepository, ReduceUrlRepository>();
    }
}
