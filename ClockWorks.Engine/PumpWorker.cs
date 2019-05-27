using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JVH.ClockWorks.Engine
{
    internal class PumpWorker
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
                ThreadPool.QueueUserWorkItem(StartPumping, cancellationToken);
            }
        }

        public bool StopPumping()
        {
            throw new NotImplementedException();
        }

        private void StartPumping(object state)
        {

        }
    }
}
