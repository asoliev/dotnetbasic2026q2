using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainstormSessions.Api;
using BrainstormSessions.Controllers;
using BrainstormSessions.Core.Interfaces;
using BrainstormSessions.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BrainstormSessions.Test.UnitTests;

public class LoggingTests : IDisposable
{
    public void Dispose()
    {
    }

    [Fact]
    public async Task HomeController_Index_LogInfoMessages()
    {
        // Arrange
        var mockRepo = new Mock<IBrainstormSessionRepository>();
        mockRepo.Setup(repo => repo.ListAsync())
            .ReturnsAsync(GetTestSessions());
        var mockLogger = new Mock<ILogger<HomeController>>();
        var controller = new HomeController(mockRepo.Object, mockLogger.Object);

        // Act
        IActionResult result = await controller.Index();

        // Assert
        VerifyLog(mockLogger, LogLevel.Information, Times.AtLeastOnce());
    }

    [Fact]
    public async Task HomeController_IndexPost_LogWarningMessage_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockRepo = new Mock<IBrainstormSessionRepository>();
        mockRepo.Setup(repo => repo.ListAsync())
            .ReturnsAsync(GetTestSessions());
        var mockLogger = new Mock<ILogger<HomeController>>();
        var controller = new HomeController(mockRepo.Object, mockLogger.Object);
        controller.ModelState.AddModelError("SessionName", "Required");
        var newSession = new HomeController.NewSessionModel();

        // Act
        IActionResult result = await controller.Index(newSession);

        // Assert
        VerifyLog(mockLogger, LogLevel.Error, Times.AtLeastOnce());
    }

    [Fact]
    public async Task IdeasController_CreateActionResult_LogErrorMessage_WhenModelStateIsInvalid()
    {
        // Arrange & Act
        var mockRepo = new Mock<IBrainstormSessionRepository>();
        var mockLogger = new Mock<ILogger<IdeasController>>();
        var controller = new IdeasController(mockRepo.Object, mockLogger.Object);
        controller.ModelState.AddModelError("error", "some error");

        // Act
        ActionResult<BrainstormSession> result = await controller.CreateActionResult(model: null);

        // Assert
        VerifyLog(mockLogger, LogLevel.Error, Times.AtLeastOnce());
    }

    [Fact]
    public async Task SessionController_Index_LogDebugMessages()
    {
        // Arrange
        int testSessionId = 1;
        var mockRepo = new Mock<IBrainstormSessionRepository>();
        mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
            .ReturnsAsync(GetTestSessions().FirstOrDefault(
                s => s.Id == testSessionId));
        var mockLogger = new Mock<ILogger<SessionController>>();
        var controller = new SessionController(mockRepo.Object, mockLogger.Object);

        // Act
        IActionResult result = await controller.Index(testSessionId);

        // Assert
        VerifyLog(mockLogger, LogLevel.Debug, Times.Exactly(2));
    }

    private static void VerifyLog<T>(Mock<ILogger<T>> loggerMock, LogLevel logLevel, Times times)
    {
        loggerMock.Verify(
            logger => logger.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((_, _) => true),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            times);
    }

    private static List<BrainstormSession> GetTestSessions()
    {
        return [
            new()
            {
                DateCreated = new DateTime(2016, 7, 2),
                Id = 1,
                Name = "Test One"
            },
            new()
            {
                DateCreated = new DateTime(2016, 7, 1),
                Id = 2,
                Name = "Test Two"
            }
        ];
    }

}
