using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an ostrich.
    /// </summary>
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Ostrich class.
        /// </summary>
        /// <param name="name">The name of the ostrich.</param>
        /// <param name="age">The age of the ostrich.</param>
        /// <param name="weight">The weigh of the ostrich (in pounds).</param>
        /// <param name="gender">The gender of the ostrich.</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 30.0;
        }

        /// <summary>
        /// The platypus's display size for the cage.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                double result = (this.Age == 0) ? 0.4 : 0.8;
                return result;
            }
        }

        /// <summary>
        /// Moves by pacing.
        /// </summary>
        public override void Move()
        {
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
        }
    }
}