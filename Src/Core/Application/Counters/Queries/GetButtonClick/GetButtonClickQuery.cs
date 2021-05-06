using Application.Common.Interfaces;
using Application.Common.TestUtilities;
using Application.Counters.Exceptions;
using Common.TestUtilities;
using Domain.Enums;
using Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var counter = await _rsDbContext.Counters.FirstOrDefaultAsync(c => c.CounterId == (int)CounterId.ButtonClicks, cancellationToken);

                if (counter == null) throw new CounterNotFoundExcpetion((int)CounterId.ButtonClicks);

                return counter.Count;
            }
        }
    }

    public class GetButtonClickQueryHandlerTests
    {
        private readonly IRsDbContext _rsDbContext;
        private GetButtonClickQuery.GetButtonClickQueryHandler _handler;

        public GetButtonClickQueryHandlerTests()
        {
            _rsDbContext = new CommandQueryTestFixture().Context;
            _handler = new GetButtonClickQuery.GetButtonClickQueryHandler(_rsDbContext);
        }

        [Fact]
        public void Hander_WhenCounterDoesNotExistInContext_ThrowsCounterNotFoundExcpetion() =>
            AssertionUtilities.AssertAsyncMethodThrows<CounterNotFoundExcpetion>(_handler.Handle(new GetButtonClickQuery()));

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task Hander_returnsButtonClickCount(int testValue)
        {
            var counter = new Counter() { CounterId = (int)CounterId.ButtonClicks, Count = testValue };
            _rsDbContext.Counters.Add(counter);
            await _rsDbContext.SaveChangesAsync();

            (await _handler.Handle(new GetButtonClickQuery())).Should().Be(testValue);
        }
    }
}
