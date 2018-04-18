using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class used to represent the three states of climbing.
    /// </summary>
    [Serializable]
    public enum ClimbProcess
    {
        Climbing,
        Falling,
        Scurrying
    }
}
