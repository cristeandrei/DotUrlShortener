using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using ReduceUrl.Data.DbContexts;

namespace ReduceUrl.Data;

internal static class ReduceUrlDataExtensions
{
    public static void AddDatabase(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDataSource(connectionName: "postgresdb");

        builder.Services.AddDbContextPool<ReduceUrlDbContext>(
            (s, o) =>
            {
                var npgsqlDataSource = s.GetRequiredService<NpgsqlDataSource>();

                o.UseNpgsql(npgsqlDataSource);
            }
        );
    }
}
