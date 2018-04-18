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
    [Serializable]
    public class HoverBehavior : IMoveBehavior
    {
        /// <summary>
        /// The state of hovering the hoverer is in.
        /// </summary>
        private HoverProcess process;

        /// <summary>
        /// A random field.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// The hoverer's step count.
        /// </summary>
        private int stepCount;

        /// <summary>
        /// The hoverer moves.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        public void Move(Animal animal)
        {
            // if there are no more steps to take (step count is at 0), switch to the next process
            if (stepCount == 0)
            {
                this.NextProcess(animal);
            }

            // decrement the step count
            stepCount--;

            // define a move distance variable
            int moveDistance;

            // if the current process is hovering
            if (this.process == HoverProcess.Hovering)
            {
                // the animal moves at a normal pace, so set the move distance variable to the animal's move distance
                moveDistance = animal.MoveDistance;
                // the animal moves randomly on each step, so give the animal a random horizontal and vertical direction
                // set the animal's horizontal and vertical directions to a random direction
                int randomX = random.Next(0, 2);
                int randomY = random.Next(0, 2);
                if (randomX == 0)
                {
                    animal.XDirection = Utilities.HorizontalDirection.Left;
                }
                else
                {
                    animal.XDirection = Utilities.HorizontalDirection.Right;
                }

                if (randomY == 0)
                {
                    animal.YDirection = Utilities.VerticalDirection.Up;
                }
                else
                {
                    animal.YDirection = Utilities.VerticalDirection.Down;
                }
            }
            else
            {
                // the animal moves at a quadruple pace, so set the move distance variable to 4 times 
                // the animal's move distance
                moveDistance = animal.MoveDistance * 4;
            }

            // move horizontally and vertically using the move distance variable
            MoveHelper.MoveHorizontally(animal, moveDistance);
            MoveHelper.MoveVertically(animal, moveDistance);
        }

        /// <summary>
        /// Moves to the next process.
        /// </summary>
        /// <param name="animal">The animal whose process switches.</param>
        private void NextProcess(Animal animal)
        {
            // if the current process is hovering
            if (this.process == HoverProcess.Hovering)
            {
                // switch to zooming
                this.process = HoverProcess.Zooming;

                // set the step count to a random number between 5 and 8, inclusive
                stepCount = random.Next(5, 9);

                // set the animal's horizontal and vertical directions to a random direction
                int randomX = random.Next(0, 2);
                int randomY = random.Next(0, 2);
                if (randomX == 0)
                {
                    animal.XDirection = Utilities.HorizontalDirection.Left;
                }
                else
                {
                    animal.XDirection = Utilities.HorizontalDirection.Right;
                }

                if (randomY == 0)
                {
                    animal.YDirection = Utilities.VerticalDirection.Up;
                }
                else
                {
                    animal.YDirection = Utilities.VerticalDirection.Down;
                }
            }
            else
            {
                // switch to hovering
                this.process = HoverProcess.Hovering;
                // set the step count to a random number between 7 and 10, inclusive
                stepCount = random.Next(7, 11);
            }
        }
    }
}
