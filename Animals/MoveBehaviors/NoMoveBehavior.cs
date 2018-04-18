using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class used to represent not moving.
    /// </summary>
    [Serializable]
    public class NoMoveBehavior : IMoveBehavior
    {
        public void Move(Animal animal)
        {
            // The animal stays still.
        }
    }
}
