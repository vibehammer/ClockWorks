using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.Core.JobQueues;

namespace JVH.ClockWorks.Engine
{
    internal abstract class PumpWorker
    {
        private bool started = false;
        private CancellationToken cancellationToken;
        private static object lockOnThis = new object();

        public void StartPumping()
        {
            lock (lockOnThis)
            {
                if (started)
                {
                    return;
                }
                
                started = true;
                cancellationToken = new CancellationToken(false);
                ThreadPool.QueueUserWorkItem(Pump, cancellationToken);
            }
        }

        public bool StopPumping()
        {
            throw new NotImplementedException();
        }

        protected abstract void Pump(object state);
    }
}
