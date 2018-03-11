using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a fish.
    /// </summary>
    public abstract class Fish : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Fish class.
        /// </summary>
        /// <param name="name">The name of the fish.</param>
        /// <param name="age">The age of the fish.</param>
        /// <param name="weight">The weight of the fish.</param>
        /// <param name="gender">The gender of the fish.</param>
        public Fish(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
        }

        /// <summary>
        /// The percentage of weight the fish gains through eating.
        /// </summary>
        protected override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }

        /// <summary>
        /// Moves by swimming.
        /// </summary>
        public override void Move()
        {
            // ~~ Horizontal ~~
            // Checks if it's moving right.
            if (this.XDirection == HorizontalDirection.Right)
            {
                // Checks if the distance to be moved reaches out of bounds. 
                // If it is, restricts movement to in-bounds and the entity turns around.
                if (this.XPosition + this.MoveDistance > this.XPositionMax)
                {
                    this.XPosition = this.XPositionMax;
                    this.XDirection = HorizontalDirection.Left;
                }
                // If it isn't, the entity moves the set distance unhindered.
                else
                {
                    this.XPosition += this.MoveDistance;
                }
            }
            // Checks if it's moving left.
            else
            {
                if (this.XPosition - this.MoveDistance < 0)
                {
                    this.XPosition = 0;
                    this.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    this.XPosition -= this.MoveDistance;
                }
            }

            // ~~ Vertical ~~
            // Checks if it's moving up.
            if (this.YDirection == VerticalDirection.Up)
            {
                // Checks if the distance to be moved reaches out of bounds. 
                // If it is, restricts movement to in-bounds and the entity turns around.
                if (this.YPosition + this.MoveDistance > this.YPositionMax)
                {
                    this.YPosition = this.YPositionMax;
                    this.YDirection = VerticalDirection.Down;
                }
                // If it isn't, the entity moves the set distance unhindered.
                else
                {
                    this.YPosition += this.MoveDistance;
                }
            }
            // Checks if it's moving down.
            else
            {
                if (this.YPosition - this.MoveDistance < 0)
                {
                    this.YPosition = 0;
                    this.YDirection = VerticalDirection.Up;
                }
                else
                {
                    this.YPosition -= this.MoveDistance;
                }
            }
        }
    }
}