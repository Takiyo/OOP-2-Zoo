using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class used to represent a move helper.
    /// </summary>
    public static class MoveHelper
    {
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
                    animal.XPosition += moveDistance;
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
                    animal.XPosition -= moveDistance;
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
                    animal.YPosition += moveDistance;
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
                    animal.YPosition -= moveDistance;
                }
            }
        }

    }
}

