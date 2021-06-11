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

namespace Application.IpAddresses.Queries.GetTotalIpAddressCount
{
    public sealed record GetTotalIpAddressCountQuery : IRequest<int>
    {
        internal sealed class GetTotalIpAddressCountQueryHandler : IRequestHandler<GetTotalIpAddressCountQuery, int>
        {
            private readonly IRsDbContext _rsDbContext;

            public GetTotalIpAddressCountQueryHandler(IRsDbContext rsDbContext)
            {
                _rsDbContext = rsDbContext;
            }

            public async Task<int> Handle(GetTotalIpAddressCountQuery request, CancellationToken cancellationToken = default)
            {
                return await _rsDbContext.IpAddresses.CountAsync(cancellationToken);
            }
        }
    }

    public class GetTotalIpAddressCountQueryHandlerTests
    {
        private readonly IRsDbContext _rsDbContext;
        private GetTotalIpAddressCountQuery.GetTotalIpAddressCountQueryHandler _handler;

        public GetTotalIpAddressCountQueryHandlerTests()
        {
            _rsDbContext = new CommandQueryTestFixture().Context;
            _handler = new GetTotalIpAddressCountQuery.GetTotalIpAddressCountQueryHandler(_rsDbContext);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task ReturnsTotalCountOfIps(int testValue)
        {
            await AddIpToContext(testValue);

            (await _handler.Handle(new GetTotalIpAddressCountQuery())).Should().Be(testValue);
        }

        private async Task AddIpToContext(int numberOfIpToAdd) 
        {
            for(int i = 0; i < numberOfIpToAdd; i++)
                _rsDbContext.IpAddresses.Add(new IpAddress() { Ip = RandomStringUtilities.RandomAlphaNumericString() });
            await _rsDbContext.SaveChangesAsync();
        }
    }
}