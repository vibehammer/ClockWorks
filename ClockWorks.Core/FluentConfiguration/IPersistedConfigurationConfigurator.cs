using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JVH.ClockWorks.Core.FluentConfiguration
{
    public interface IPersistedConfigurationConfigurator
    {
        List<JobDescriptor> Configure(string configurationFile);
    }
}
