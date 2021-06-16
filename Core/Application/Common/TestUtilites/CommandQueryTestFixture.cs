using Common;
using Moq;
using System;

namespace Application.Common.TestUtilities
{
    public class CommandQueryTestFixture
    {
        public IDateTimeProvider DateTimeProvider { get; }
        public RsTestDbContext Context { get; }

        public CommandQueryTestFixture()
        {
            var mockDateTime = new Mock<IDateTimeProvider>();
            mockDateTime.Setup(dtp => dtp.Now).Returns(new DateTime(3000, 1, 1));
            DateTimeProvider = mockDateTime.Object;

            Context = RsDbContextFactory.Create();
        }
    }
}
