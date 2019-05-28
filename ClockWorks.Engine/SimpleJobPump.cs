using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using JVH.ClockWorks.Core.JobQueues;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.Engine
{
    internal class SimpleJobPump : PumpWorker
    {
        private readonly IJobQueue<SimpleJobDescription, SimpleJobDescription> simpleJobQueue;
        private readonly Timer timer;
        public SimpleJobPump(IJobQueue<SimpleJobDescription, SimpleJobDescription> simpleJobQueue)
        {
            this.simpleJobQueue = simpleJobQueue ?? throw new ArgumentNullException(nameof(simpleJobQueue));
            timer = new Timer();
            
        }

        protected override void Pump(object state)
        {
            timer.
            throw new NotImplementedException();
        }
    }
}
