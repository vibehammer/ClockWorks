using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JVH.ClockWorks.Engine
{
    internal class JobPump : IJobPump
    {
        private readonly PumpWorker worker = new PumpWorker();
        private CancellationToken cancellationToken;

        public void Start()
        {
            if (worker == null)
            {
                worker.StartPumping();
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }



    }
}
