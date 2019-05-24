using JVH.ClockWorks;
using System;
using System.Threading.Tasks;

namespace ClockWorks.Tests
{
    internal class TestJob : IJob
    {
        public Task<bool> Execute(JobData data)
        {
            throw new NotImplementedException();
        }
    }
}
