using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent flying.
    /// </summary>
    [Serializable]
    public class FlyBehavior : IMoveBehavior
    {
        private int evenOrOdd = 2;

        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            // Simulates wing flapping. Every time the bird is drawn,
            // the bird goes up or down 10 units.
            if (evenOrOdd % 2 == 0)
            {
                MoveHelper.MoveVertically(animal, 10);
                evenOrOdd++;
            }
            else if (evenOrOdd % 2 != 0)
            {
                MoveHelper.MoveVertically(animal, -10);
                evenOrOdd++;
            }
        }
    }
}
