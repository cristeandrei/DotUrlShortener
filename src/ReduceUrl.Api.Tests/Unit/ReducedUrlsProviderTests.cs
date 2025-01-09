using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReduceUrl.Data.DbContexts;
using ReduceUrl.Data.Models;

namespace ReduceUrl.Api.Tests.Unit;

[TestClass]
public sealed class ReducedUrlsProviderTests
{
    [TestMethod]
    public async Task ShouldReturnTheReducedUrls()
    {
        await using var connection = new SqliteConnection("DataSource=:memory:");

        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder().UseSqlite(connection).Options;

        await using var reduceUrlDbContext = new ReduceUrlDbContext(options);

        await reduceUrlDbContext.Database.EnsureCreatedAsync();

        reduceUrlDbContext.ReducedUrls.Add(
            new ReducedUrl { Path = "hello", CreationDateTime = DateTime.MinValue }
        );

        await reduceUrlDbContext.SaveChangesAsync();

        var reduceUrlRepository = new ReduceUrlRepository(reduceUrlDbContext);

        var forecasts = await reduceUrlRepository.GetAll();

        forecasts.Should().NotBeEmpty();
    }
}
