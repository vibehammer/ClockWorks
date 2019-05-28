using System;
using System.Collections.Generic;
using System.Text;

namespace JVH.ClockWorks.Core.TriggerDescriptions
{
    public enum TriggerType
    {
        Unknown = 0,
        Delayed,
        JobFinished,
        ExactTime,
        TimeOfDay
    }

}
