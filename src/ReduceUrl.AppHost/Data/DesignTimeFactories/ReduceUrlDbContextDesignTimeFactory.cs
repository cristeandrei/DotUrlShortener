using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ReduceUrl.Data.DbContexts;

namespace ReduceUrl.AppHost.Data.DesignTimeFactories;

internal sealed class ReduceUrlDbContextDesignTimeFactory
    : IDesignTimeDbContextFactory<ReduceUrlDbContext>
{
    public ReduceUrlDbContext CreateDbContext(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        builder.AddPostgres("postgres").AddDatabase("migrations", databaseName: "migrations");

        var optionsBuilder = new DbContextOptionsBuilder<ReduceUrlDbContext>();

        optionsBuilder.UseNpgsql("migrations");

        return new ReduceUrlDbContext(optionsBuilder.Options);
    }
}
