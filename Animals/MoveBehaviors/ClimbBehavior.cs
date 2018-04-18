using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class used to represent climbing.
    /// </summary>
    [Serializable]
    public class ClimbBehavior : IMoveBehavior
    {
        /// <summary>
        /// The climber's state.
        /// </summary>
        private ClimbProcess process;

        /// <summary>
        /// A random field.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// The climber's max height.
        /// </summary>
        private int maxHeight;

        /// <summary>
        /// The climber moves.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        public void Move(Animal animal)
        {
            // if the current process is climbing
            if (process == ClimbProcess.Climbing)
            {
                // ensure animal is moving up
                if (animal.YDirection == VerticalDirection.Up)
                {
                    animal.YDirection = VerticalDirection.Down;
                }
                // move vertically
                MoveHelper.MoveVertically(animal, animal.MoveDistance);
                // if the animal's next step will take it above the max height
                if (animal.YPosition - animal.MoveDistance <= maxHeight)
                {
                    // make animal move down
                    animal.YDirection = VerticalDirection.Up;
                    // switch its horizontal direction (if moving right, make it move left and vice versa)
                    if (animal.XDirection == HorizontalDirection.Right)
                    {
                        animal.XDirection = HorizontalDirection.Left;
                    }
                    else
                    {
                        animal.XDirection = HorizontalDirection.Right;
                    }
                    // switch to next process
                    this.NextProcess(animal);
                }
            }
            // if current process is falling
            if (process == ClimbProcess.Falling)
            {
                // move horizontally
                MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
                // move vertically at twice the distance
                MoveHelper.MoveVertically(animal, animal.MoveDistance * 2);
                // if the animal's next move will take it past the bottom, switch to the next process
                if (animal.YPosition + (animal.MoveDistance * 2) >= animal.YPositionMax)
                {
                    this.NextProcess(animal);
                }
            }
            // if the current process is scurrying
            if (process == ClimbProcess.Scurrying)
            {
                // move horizontally
                MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
                // if the animal will hit a vertical wall (either right or left), set the animal to the edge and switch to the next process
                if (animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                {
                    animal.XPosition = animal.XPositionMax;
                    this.NextProcess(animal);
                }
                if (animal.XPosition - animal.MoveDistance <= 0)
                {
                    animal.XPosition = 0;
                    this.NextProcess(animal);
                }
            }
        }

        /// <summary>
        /// Moves to the next process.
        /// </summary>
        /// <param name="animal">The animal whose process switches.</param>
        private void NextProcess(Animal animal)
        {
            // if the current process is climbing, switch to falling
            if (this.process == ClimbProcess.Climbing)
            {
                this.process = ClimbProcess.Falling;
            }
            // if the current process is falling, switch to scurrying
            else if (this.process == ClimbProcess.Falling)
            {
                this.process = ClimbProcess.Scurrying;
            }
            // if the current process is scurrying
            else if (this.process == ClimbProcess.Scurrying)
            {
                int lowerMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.15));
                int higherMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.85));

                // set max height to a random value between the lowest max and the highest max
                // switch to climbing
                maxHeight = random.Next(lowerMax, higherMax);
                this.process = ClimbProcess.Climbing;
            }
        }
    }
}
