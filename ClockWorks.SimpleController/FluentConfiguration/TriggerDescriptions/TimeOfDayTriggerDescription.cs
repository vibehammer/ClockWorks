using System;
using System.Collections.Generic;
using System.Text;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class TimeOfDayTriggerDescription : TriggerDescription
    {
        public TimeSpan TimeOfDay { get; set; }
    }
}
