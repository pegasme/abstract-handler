using FluentAssertions;
using FluentResults;
using Moq;
using Serilog;
using System.Text;
using Xm.TestTask.Services;
using Xm.TestTask.Services.Strategies;

namespace Xm.TestTask.Messages.UnitTests
{
    public class ProcessMessageServiceTests
    {
        private readonly Mock<ILogger> _logger;
        private readonly byte[] _data;

        public ProcessMessageServiceTests()
        {
            _logger = new Mock<ILogger>();
            _data = Encoding.ASCII.GetBytes("Test");
        }

        [Test]
        [TestCase("test")]
        [TestCase("")]
        [TestCase("  ")]
        public async Task ProcessMessageAsync_ShouldFail_IfEventDoesntFindByName(string dataType)
        {
            // arrange
            var actionHandler = new ActionHandler(_logger.Object);
            var notificationHandler = new NotificationHandler(_logger.Object);

            var handlers = new List<IMessageHandler> { actionHandler, notificationHandler };

            var service = new ProcessMessageService(handlers, _logger.Object);

            // act
            var result = await service.ProcessMessageAsync(dataType, _data);

            // assert
            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public async Task ProcessMessageAsync_ShouldFail_IfStrategyFailed()
        {
            // arrange
            var handlerMock = new Mock<IMessageHandler>();

            handlerMock.Setup(s => s.DataType).Returns("Test");
            handlerMock.Setup(s => s.HandleAsync(It.IsAny<byte[]>())).ReturnsAsync(Result.Fail("Ooops"));

            var handlers = new List<IMessageHandler> { handlerMock.Object };

            var service = new ProcessMessageService(handlers, _logger.Object);

            // act
            var result = await service.ProcessMessageAsync(handlerMock.Object.DataType, _data);

            // assert
            result.IsFailed.Should().BeTrue();
            handlerMock.Verify(s => s.HandleAsync(It.IsAny<byte[]>()), Times.Once());
        }

        [Test]
        public async Task ProcessMessageAsync_ShouldReturnsOk_IfStrategyFoundAndRetunsOk()
        {
            // arrange
            var handlerMock = new Mock<IMessageHandler>();

            handlerMock.Setup(s => s.DataType).Returns("Test");
            handlerMock.Setup(s => s.HandleAsync(It.IsAny<byte[]>())).ReturnsAsync(Result.Ok());

            var handlers = new List<IMessageHandler> { handlerMock.Object };

            var service = new ProcessMessageService(handlers, _logger.Object);

            // act
            var result = await service.ProcessMessageAsync(handlerMock.Object.DataType, _data);

            // assert
            result.IsSuccess.Should().BeTrue();
            handlerMock.Verify(s => s.HandleAsync(It.IsAny<byte[]>()), Times.Once());
        }
    }
}