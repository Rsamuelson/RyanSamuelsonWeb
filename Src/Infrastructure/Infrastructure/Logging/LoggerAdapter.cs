using Application.Common.TestUtilties;
using Common;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        public readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogError(string message, params object[] args) =>
            _logger.LogError(message, args);

        public void LogInformation(string message, params object[] args) =>
            _logger.LogInformation(message, args);

        public void LogWarning(string message, params object[] args) =>
            _logger.LogWarning(message, args);
    }

    public class LoggerAdapterTests
    {
        public class TestClass { }

        private readonly Mock<ILoggerFactory> _mockLoggerFactory;
        private readonly Mock<ILogger<TestClass>> _mockLogger;
        private readonly LoggerAdapter<TestClass> _loggerAdapter;

        public LoggerAdapterTests()
        {
            _mockLoggerFactory = new Mock<ILoggerFactory>();
            _mockLogger = new Mock<ILogger<TestClass>>();
            _loggerAdapter = new LoggerAdapter<TestClass>(_mockLoggerFactory.Object);

            _mockLoggerFactory.Setup(m => m.CreateLogger(It.IsAny<string>())).Returns(_mockLogger.Object);
        }

        [Fact]
        public void LogError_LoggerLogErrorIsCalled()
        {
            var testError = RandomStringUtilities.RandomAlphaNumericString(10);
            object[] testArgs = new List<object>() { RandomStringUtilities.RandomAlphaNumericString(10) }.ToArray();
            _loggerAdapter.LogError(testError, testArgs);

            _mockLogger.Object.

            _mockLogger.Verify(m => m.LogError(testError, testArgs), Times.Once);
        }

        [Fact]
        public void LogInformation_LoggerLogInformationIsCalled()
        {
            var testError = RandomStringUtilities.RandomAlphaNumericString(10);
            _loggerAdapter.LogInformation(testError);

            _mockLogger.Verify(m => m.LogInformation(testError), Times.Once);
        }

        [Fact]
        public void LogWarning_LoggerLogWarningIsCalled()
        {
            var testError = RandomStringUtilities.RandomAlphaNumericString(10);
            _loggerAdapter.LogWarning(testError);

            _mockLogger.Verify(m => m.LogWarning(testError), Times.Once);
        }
    }
}
