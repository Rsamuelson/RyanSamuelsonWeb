using Application.Common.Interfaces;
using Application.Common.TestUtilities;
using Domain.Enums;
using Domain.Models;
using FluentAssertions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Counters.Queries.GetButtonClick
{
    public record GetButtonClickQuery : IRequest<int>
    {
        internal class GetButtonClickQueryHandler : IRequestHandler<GetButtonClickQuery, int>
        {
            private readonly IRsDbContext _rsDbContext;

            public GetButtonClickQueryHandler(IRsDbContext rsDbContext)
            {
                _rsDbContext = rsDbContext;
            }

            public async Task<int> Handle(GetButtonClickQuery request, CancellationToken cancellationToken = default)
            {
                var counter = await _rsDbContext.Counters.FindAsync(new object[] { (int)CounterId.ButtonClicks }, cancellationToken);

                return counter.Count;
            }
        }
    }

    public class GetButtonClickQueryHandlerTests : IClassFixture<CommandQueryTestFixture>, IDisposable
    {
        private readonly IRsDbContext _rsDbContext;
        private GetButtonClickQuery.GetButtonClickQueryHandler _handler;

        public GetButtonClickQueryHandlerTests(CommandQueryTestFixture commandQueryTestFixture)
        {
            _rsDbContext = commandQueryTestFixture.Context;
            _handler = new GetButtonClickQuery.GetButtonClickQueryHandler(_rsDbContext);
        }

        public void Dispose()
        {
            _rsDbContext.Dispose();
        }

        [Fact]
        public async Task Hander_returnsButtonClickCount()
        {
            var counter = new Counter() { CounterId = (int)CounterId.ButtonClicks, Count = int.MaxValue };
            _rsDbContext.Counters.Add(counter);
            await _rsDbContext.SaveChangesAsync();

            (await _handler.Handle(new GetButtonClickQuery())).Should().Be(counter.Count);
        }
    }
}
