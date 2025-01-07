using Bunit;
using DotUrlShortener.Ui.Components.Pages;
using TestContext = Bunit.TestContext;

namespace DotUrlShortener.Ui.Tests.Unit;

[TestClass]
public class HelloWorldTest : TestContext
{
    [TestMethod]
    public void HelloWorldComponentRendersCorrectly()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        cut.MarkupMatches(
            """
            <h1>Hello, world!</h1>
            Welcome to your new app.
            """
        );
    }
}
