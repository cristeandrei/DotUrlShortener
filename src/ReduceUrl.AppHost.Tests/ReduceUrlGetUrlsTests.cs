using System.Net;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ReduceUrl.AppHost.Tests;

[TestClass]
public class ReduceUrlGetUrlsTests
{
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetUrlsReturnsOkStatusCode()
    {
        // Arrange
        var appHost =
            await DistributedApplicationTestingBuilder.CreateAsync<Projects.ReduceUrl_AppHost>();

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        await using var app = await appHost.BuildAsync();

        var resourceNotificationService =
            app.Services.GetRequiredService<ResourceNotificationService>();

        await app.StartAsync();

        // Act
        using var httpClient = app.CreateHttpClient("reduceurlui");

        await resourceNotificationService
            .WaitForResourceAsync(
                "reduceurldataaspiremigration",
                KnownResourceStates.TerminalStates
            )
            .WaitAsync(TimeSpan.FromSeconds(30));

        await resourceNotificationService
            .WaitForResourceAsync("reduceurlui", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));

        var requestUri = new Uri("/Urls", UriKind.Relative);

        var response = await httpClient.GetAsync(requestUri);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
}
