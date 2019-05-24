using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class JobFinishedTriggerDescription : TriggerDescription
    {
        public virtual string JobId { get; set; }
        public virtual bool OnlyIfSuccessfull { get; set; }

    }
}
