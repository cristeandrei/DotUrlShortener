using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using ReduceUrl.Data.Models;
using ReduceUrl.Data.Repositories.Interfaces;

namespace ReduceUrl.Api.Tests;

[TestClass]
public sealed class ReducedUrlsEndpointTests
{
    [TestMethod]
    public async Task ShouldHaveASuccessfulStatusCode()
    {
        await using var webApplicationFactory = new WebApplicationFactory<Program>();

        var reduceUrlRepository = Mock.Of<IReduceUrlRepository>(e =>
            e.GetAll(CancellationToken.None).Result == new List<ReducedUrl>()
        );

        var httpClient = webApplicationFactory
            .WithWebHostBuilder(e =>
            {
                e.ConfigureTestServices(a =>
                {
                    a.RemoveAll<IReduceUrlRepository>();
                    a.AddScoped<IReduceUrlRepository>(_ => reduceUrlRepository);
                });
            })
            .CreateClient();

        var uri = new Uri("/reducedUrls", UriKind.Relative);

        var response = await httpClient.GetAsync(uri);

        Assert.AreEqual(true, response.IsSuccessStatusCode);
    }
}
