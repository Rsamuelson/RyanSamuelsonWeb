using Application.Common.Interfaces;
using Application.Common.TestUtilities;
using Common.TestUtilities;
using Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IpAddresses.Queries.DoesIpAddressExists
{
    public sealed record DoesIpAddressExistsQuery : IRequest<bool>
    {
        public DoesIpAddressExistsQuery(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        private string IpAddress { get; }

        internal sealed class DoesIpAddressExistsQueryHandler : IRequestHandler<DoesIpAddressExistsQuery, bool>
        {
            private readonly IRsDbContext _rsDbContext;

            public DoesIpAddressExistsQueryHandler(IRsDbContext rsDbContext)
            {
                _rsDbContext = rsDbContext;
            }

            public async Task<bool> Handle(DoesIpAddressExistsQuery request, CancellationToken cancellationToken = default)
            {
                return await _rsDbContext.IpAddresses.AnyAsync(ipa => ipa.Ip == request.IpAddress, cancellationToken);
            }
        }
    }

    public class DoesIpAddressExistsQueryHandlerTests
    {
        private readonly IRsDbContext _rsDbContext;
        private DoesIpAddressExistsQuery.DoesIpAddressExistsQueryHandler _handler;

        public DoesIpAddressExistsQueryHandlerTests()
        {
            _rsDbContext = new CommandQueryTestFixture().Context;
            _handler = new DoesIpAddressExistsQuery.DoesIpAddressExistsQueryHandler(_rsDbContext);
        }

        [Fact]
        public async Task Handler_NoIpAddressExist_ReturnsFalse()
        {
            string testIpAddress = RandomStringUtilities.RandomAlphaNumericString();

            _rsDbContext.IpAddresses.Add(new IpAddress() { Ip = testIpAddress });
            await _rsDbContext.SaveChangesAsync();

            (await _handler.Handle(new DoesIpAddressExistsQuery(RandomStringUtilities.RandomAlphaNumericString()))).Should().BeFalse();
        }

        [Fact]
        public async Task Handler_WhenIpAddressDoesNotExist_ReturnsFalse()
        {
            string testIpAddress = RandomStringUtilities.RandomAlphaNumericString();

            _rsDbContext.IpAddresses.Add(new IpAddress() { Ip = testIpAddress });
            await _rsDbContext.SaveChangesAsync();

            (await _handler.Handle(new DoesIpAddressExistsQuery(RandomStringUtilities.RandomAlphaNumericString()))).Should().BeFalse();
        }

        [Fact]
        public async Task Handler_WhenIpAddressExists_ReturnsTrue()
        {
            string testIpAddress = RandomStringUtilities.RandomAlphaNumericString();

            _rsDbContext.IpAddresses.Add(new IpAddress() { Ip = testIpAddress });
            await _rsDbContext.SaveChangesAsync();

            (await _handler.Handle(new DoesIpAddressExistsQuery(testIpAddress))).Should().BeTrue();
        }
    }
}