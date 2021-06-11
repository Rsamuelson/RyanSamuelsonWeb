using Application.Common.Interfaces;
using Application.Common.TestUtilities;
using Common.TestUtilities;
using Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IpAddresses.Commands.AddIpAddress
{
    public sealed record AddIpCommand : IRequest
    {
        public AddIpCommand(AddIpViewModel addIpViewModel)
        {
            ViewModel = addIpViewModel;
        }

        public AddIpViewModel ViewModel { get; }

        internal sealed class AddIpCommandHandler : IRequestHandler<AddIpCommand>
        {
            private readonly IRsDbContext _rsDbContext;

            public AddIpCommandHandler(IRsDbContext rsDbContext)
            {
                _rsDbContext = rsDbContext;
            }

            public async Task<Unit> Handle(AddIpCommand request, CancellationToken cancellationToken = default)
            {
                _rsDbContext.IpAddresses.Add(new IpAddress() { Ip = request.ViewModel.IpAddress });
                await _rsDbContext.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }

    public class AddIpCommandHandlerTests
    {
        private readonly IRsDbContext _rsDbContext;
        private AddIpCommand.AddIpCommandHandler _handler;

        public AddIpCommandHandlerTests()
        {
            _rsDbContext = new CommandQueryTestFixture().Context;
            _handler = new AddIpCommand.AddIpCommandHandler(_rsDbContext);
        }

        [Fact]
        public async Task Handler_NewIpAddressIsAddedToContext()
        {
            string testIpAddress = RandomStringUtilities.RandomAlphaNumericString();

            await _handler.Handle(new AddIpCommand(new AddIpViewModel { IpAddress = testIpAddress }));

            _rsDbContext.IpAddresses.First().Ip.Should().Be(testIpAddress);
        }

        [Fact]
        public async Task Handler_WhenIpAddressExists_NoNewIpIsAdded()
        {
            string testIpAddress = RandomStringUtilities.RandomAlphaNumericString();

            _rsDbContext.IpAddresses.Add(new IpAddress() { Ip = testIpAddress });
            await _rsDbContext.SaveChangesAsync();

            await _handler.Handle(new AddIpCommand(new AddIpViewModel { IpAddress = testIpAddress }));

            _rsDbContext.IpAddresses.Count().Should().Be(1);
        }
    }
}
