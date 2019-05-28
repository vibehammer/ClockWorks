using JVH.ClockWorks.Core;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class JobFinishedTriggerDescription : TriggerDescription, IDeepCloneable<JobFinishedTriggerDescription>
    {
        public virtual string JobId { get; set; }
        public virtual bool OnlyIfSuccessfull { get; set; }

        public JobFinishedTriggerDescription DeepClone() => 
            new JobFinishedTriggerDescription {JobId = this.JobId, OnlyIfSuccessfull = this.OnlyIfSuccessfull};
    }
}
