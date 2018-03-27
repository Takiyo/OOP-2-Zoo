using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class used to represent climbing.
    /// </summary>
    public class ClimbBehavior : IMoveBehavior
    {
        /// <summary>
        /// The climber moves.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        public void Move(Animal animal)
        {
            // Check if the squirrel's Y position does not equal zero && the squirrel is not on a horizontal border.
                // If airborne, squirrel descends at steep angle until they hit the ground. X:Y 1:2 or 1:3 ratio.
            // Check if the squirrel is on a horizontal border.
                // If climbing, squirrel climbs until its Y position matches a random number in a range equal to
                // the wall's height.
                    // When the random height is met, the squirrel flips horizontally and moves 1 unit away from the
                    // horizontal wall. "Airborne check" should resolve true next time Move is called.
            // If the squirrel is not in either of the 2 other states, they are on the ground.
                // If grounded, they move horizontally until they reach a border. "Climbing check" should resolve to true
                // the next time Move is called.
        }
    }
}
