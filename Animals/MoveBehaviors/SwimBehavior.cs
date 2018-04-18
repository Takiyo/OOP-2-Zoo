using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent swimming.
    /// </summary>
    [Serializable]
    public class SwimBehavior : IMoveBehavior
    {
        /// <summary>
        /// Makes an animal swim.
        /// </summary>
        /// <param name="animal">The animal to move.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
            MoveHelper.MoveVertically(animal, animal.MoveDistance / 2);
        }
    }
}
