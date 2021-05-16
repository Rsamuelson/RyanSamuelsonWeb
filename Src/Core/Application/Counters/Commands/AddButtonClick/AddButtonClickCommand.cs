using Application.Common.Interfaces;
using Application.Common.TestUtilities;
using Application.Counters.Exceptions;
using Common.TestUtilities;
using Domain.Constants;
using Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Counters.Commands.AddButtonClick
{
    public record AddButtonClickCommand : IRequest
    {
        internal class AddButtonClickCommandHandler : IRequestHandler<AddButtonClickCommand>
        {
            private readonly IRsDbContext _rsDbContext;

            public AddButtonClickCommandHandler(IRsDbContext rsDbContext)
            {
                _rsDbContext = rsDbContext;
            }

            public async Task<Unit> Handle(AddButtonClickCommand request, CancellationToken cancellationToken = default)
            {
                var counter = await _rsDbContext.Counters.FirstOrDefaultAsync(c => c.Name == CounterNames.ButtonClicks, cancellationToken);

                if (counter == null) throw new CounterNotFoundExcpetion(CounterNames.ButtonClicks);

                counter.Count++;

                await _rsDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

    public class GetButtonClickQueryHandlerTests
    {
        private readonly IRsDbContext _rsDbContext;
        private AddButtonClickCommand.AddButtonClickCommandHandler _handler;

        public GetButtonClickQueryHandlerTests()
        {
            _rsDbContext = new CommandQueryTestFixture().Context;
            _handler = new AddButtonClickCommand.AddButtonClickCommandHandler(_rsDbContext);
        }

        [Fact]
        public void Hander_WhenCounterDoesNotExistInContext_ThrowsCounterNotFoundExcpetion() =>
            AssertionUtilities.AssertAsyncMethodThrows<CounterNotFoundExcpetion>(_handler.Handle(new AddButtonClickCommand()));

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Hander_returnsButtonClickCount(int testValue)
        {
            var counter = new Counter() { Name = CounterNames.ButtonClicks, Count = testValue };
            _rsDbContext.Counters.Add(counter);
            await _rsDbContext.SaveChangesAsync();

            await _handler.Handle(new AddButtonClickCommand());

            counter.Count.Should().Be(testValue + 1);
        }
    }
}
