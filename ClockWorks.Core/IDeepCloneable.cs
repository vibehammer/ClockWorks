using System;
using System.Collections.Generic;
using System.Text;

namespace JVH.ClockWorks.Core
{
    public interface IDeepCloneable<out TClone>
    {
        TClone DeepClone();
    }
}
