using DotUrlShortener.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace DotUrlShortener.Data;

internal static class DotUrlShortenerDataExtensions
{
    public static void AddDatabase(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDataSource(connectionName: "postgresdb");

        builder.Services.AddDbContextPool<DotUrlShortenerDbContext>(
            (s, o) =>
            {
                var npgsqlDataSource = s.GetRequiredService<NpgsqlDataSource>();

                o.UseNpgsql(npgsqlDataSource);
            }
        );
    }
}
