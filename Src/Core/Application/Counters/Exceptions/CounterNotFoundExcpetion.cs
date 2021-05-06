using System;

namespace Application.Counters.Exceptions
{
    public class CounterNotFoundExcpetion : Exception
    {
        public CounterNotFoundExcpetion(int counterId) : base($"Counter {counterId} Not Found.")
        {
        }
    }
}
