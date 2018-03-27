using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class used to represent hovering
    /// </summary>
    public class HoverBehavior : IMoveBehavior
    {
        /// <summary>
        /// The hoverer moves.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        public void Move(Animal animal)
        {
            // Checks if animal is hovering and for how long they have been hovering using a hover time counter.
                // If hovering, they will randomly either switch which direction they're facing or move 1 (or a couple)
                // unit(s) in a random direction.
            // If hover counter is too high the previous condition will resolve false, this is the else.
                // If zooming, they will move in a random diagonal direction at four times the default step distance.
        }
    }
}
