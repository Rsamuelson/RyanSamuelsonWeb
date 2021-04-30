using FluentAssertions;
using FluentAssertions.Specialized;
using System;
using System.Threading.Tasks;

namespace Application.Common.TestUtilties
{
    public static class AssertionUtilities
    {
        public static ExceptionAssertions<TException> AssertAsyncMethodThrows<TException>(Task method)
            where TException : Exception
        {
            return FluentActions.Awaiting(() => method).Should().ThrowExactly<TException>();
        }
    }
}
