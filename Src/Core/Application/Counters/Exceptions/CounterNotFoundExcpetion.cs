using System;

namespace Application.Counters.Exceptions
{
    public class CounterNotFoundExcpetion : Exception
    {
        public CounterNotFoundExcpetion(string counterName) : base($"Counter {counterName} Not Found.")
        {
        }
    }
}
