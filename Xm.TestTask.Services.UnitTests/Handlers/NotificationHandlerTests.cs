using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using Xm.TestTask.Services.Strategies;

namespace Xm.TestTask.Messages.UnitTests.Strategies;

public class NotificationHandlerTests
{
    private readonly Mock<ILogger> _loggerMock;

    public NotificationHandlerTests()
    {
        _loggerMock = new Mock<ILogger>();
    }

    [Test]
    public async Task HandleAsync_ShouldFail_IfDeserializationFailed()
    {
        // arrange
        var testData = Encoding.UTF8.GetBytes("test");

        var handler = new NotificationHandler(_loggerMock.Object);

        // act
        var result = await handler.HandleAsync(testData);

        // assert
        result.IsFailed.Should().BeTrue();
    }

    [Test]
    public async Task HandleAsync_ShouldReturnOk_IfDeserializationDoesnNotFail()
    {
        // arrange
        var jsonData = JsonConvert.SerializeObject(new NotificationMessage());
        var testData = Encoding.UTF8.GetBytes(jsonData);

        var handler = new NotificationHandler(_loggerMock.Object);

        // act
        var result = await handler.HandleAsync(testData);

        // assert
        result.IsSuccess.Should().BeTrue();
    }
}
