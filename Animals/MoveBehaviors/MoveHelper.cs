using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using CagedItems;

namespace Animals
{
    /// <summary>
    /// The class used to represent a move helper.
    /// </summary>
    [Serializable]
    public static class MoveHelper
    {
        private static double hungerModifier;

        public static void MoveHorizontally(Animal animal, int moveDistance)
        {
            // Checks if it's moving right.
            if (animal.XDirection == HorizontalDirection.Right)
            {
                // Checks if the distance to be moved reaches out of bounds. 
                // If it is, restricts movement to in-bounds and the entity turns around.
                if (animal.XPosition + moveDistance > animal.XPositionMax)
                {
                    animal.XPosition = animal.XPositionMax;
                    animal.XDirection = HorizontalDirection.Left;
                }
                // If it isn't, the entity moves the set distance unhindered.
                else
                {
                    hungerModifier = CheckHungerState(animal, hungerModifier);
                    animal.XPosition += (moveDistance * Convert.ToInt32(hungerModifier));
                }
            }
            // Checks if it's moving left.
            else
            {
                if (animal.XPosition - moveDistance < 0)
                {
                    animal.XPosition = 0;
                    animal.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    hungerModifier = CheckHungerState(animal, hungerModifier);
                    animal.XPosition -= (moveDistance * Convert.ToInt32(hungerModifier));
                }
            }
        }

        public static void MoveVertically(Animal animal, int moveDistance)
        {
            // Checks if it's moving up.
            if (animal.YDirection == VerticalDirection.Up)
            {
                // Checks if the distance to be moved reaches out of bounds. 
                // If it is, restricts movement to in-bounds and the entity turns around.
                if (animal.YPosition + moveDistance > animal.YPositionMax)
                {
                    animal.YPosition = animal.YPositionMax;
                    animal.YDirection = VerticalDirection.Down;
                }
                // If it isn't, the entity moves the set distance unhindered.
                else
                {
                    hungerModifier = CheckHungerState(animal, hungerModifier);
                    animal.YPosition += (moveDistance * Convert.ToInt32(hungerModifier));
                }
            }
            // Checks if it's moving down.
            else
            {
                if (animal.YPosition - moveDistance < 0)
                {
                    animal.YPosition = 0;
                    animal.YDirection = VerticalDirection.Up;
                }
                else
                {
                    hungerModifier = CheckHungerState(animal, hungerModifier);
                    animal.YPosition -= (moveDistance * Convert.ToInt32(hungerModifier));
                }
            }
        }

        /// <summary>
        /// Checks the animal's state of hunger and adjusts movement accordingly.
        /// </summary>
        /// <param name="animal">Animal to be checked.</param>
        /// <param name="movementModifier">Movement modifier based on hunger state.</param>
        /// <returns>The modifier.</returns>
        private static double CheckHungerState(Animal animal, double movementModifier)
        {
            switch (animal.HungerState)
            {
                case HungerState.Satisfied:
                    movementModifier = 1;
                    break;

                case HungerState.Hungry:
                    movementModifier = 0.25;
                    break;

                case HungerState.Starving:
                    movementModifier = 0;
                break;

                case HungerState.Unconscious:
                    movementModifier = 0;
                    break;
            }

            return movementModifier;
        }
    }
}

