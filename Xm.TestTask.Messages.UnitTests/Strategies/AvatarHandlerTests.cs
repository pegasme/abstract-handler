using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using Xm.TestTask.Services.Strategies;

namespace Xm.TestTask.Messages.UnitTests.Strategies;

public class AvatarHandlerTests
{
    private readonly Mock<ILogger> _loggerMock;

    public AvatarHandlerTests()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [Test]
    public async Task HandleAsync_ShouldReturnOk()
    {
        // arrange
        var jsonData = JsonConvert.SerializeObject(new ActionMessage());
        var testData = Encoding.UTF8.GetBytes(jsonData);

        var handler = new AvatarHandler(_loggerMock.Object);

        // act
        var result = await handler.HandleAsync(testData);

        // assert
        result.IsSuccess.Should().BeTrue();
    }
}
