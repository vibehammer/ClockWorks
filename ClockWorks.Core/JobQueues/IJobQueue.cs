using System;
using System.Collections.Generic;
using System.Text;
using JVH.ClockWorks.Core.FluentConfiguration;

namespace JVH.ClockWorks.Core.JobQueues
{
    public interface IJobQueue<in TJobDescriptor, out TNextJob> 
        where TJobDescriptor : JobDescriptor
        where TNextJob : JobDescriptor
    {
        void AddJob(TJobDescriptor jobDescription);
        void RemoveJob(TJobDescriptor jobDescription);
        TNextJob Next();
        TNextJob PeekNext();
    }
}
