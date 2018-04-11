using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class used to represent hop behavior.
    /// </summary>
    public class HopBehavior : IMoveBehavior
    {
        /// <summary>
        /// The maximum height the jumper can jump.
        /// </summary>
        private int jumpHeight = 300;

        /// <summary>
        /// The hopper's state.
        /// </summary>
        private HopProcess process;

        /// <summary>
        /// The hopper moves.
        /// </summary>
        /// <param name="animal">The animal to be moved.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            if (this.process == HopProcess.Rising)
            {
                animal.YDirection = VerticalDirection.Up;
                MoveHelper.MoveVertically(animal, animal.MoveDistance);
                
                if (animal.YPosition <= jumpHeight)
                {
                    this.NextProcess(animal);
                }
            }
            else
            {
                animal.YDirection = VerticalDirection.Down;
                MoveHelper.MoveVertically(animal, animal.MoveDistance);

                if (animal.YPosition >= 400)
                {
                    this.NextProcess(animal);
                }
            }
        }

        /// <summary>
        /// Switches to the next process.
        /// </summary>
        /// <param name="animal">The animal who switches.</param>
        private void NextProcess(Animal animal)
        {
            if (this.process == HopProcess.Rising)
            {
                this.process = HopProcess.Falling;
            }
            else
            {
                this.process = HopProcess.Rising;
            }
        }
    }
}
