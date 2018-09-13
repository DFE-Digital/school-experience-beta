using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Moq;

namespace SchoolExperienceTestHelpers
{
    public class MockLoggerFactory : Mock<ILoggerFactory>
    {
        private readonly ILogger _logger;

        public ILogger Logger => _logger;

        public MockLoggerFactory()
        {
            var logger = new DebugLogger("Test", (string message, LogLevel level) => true);
            _logger = logger;

            Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(_logger);
        }
    }
}
