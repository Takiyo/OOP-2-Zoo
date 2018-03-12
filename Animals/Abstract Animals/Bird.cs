using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a bird.
    /// </summary>
    public abstract class Bird : Animal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Bird class.
        /// </summary>
        /// <param name="name">The name of the bird.</param>
        /// <param name="age">The age of the bird.</param>
        /// <param name="weight">The weight of the bird (in pounds).</param>
        /// <param name="gender">The gender of the bird.</param>
        public Bird(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        protected override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }

        /// <summary>
        /// Hatches from its egg.
        /// </summary>
        public void Hatch()
        {
            // Break out of egg.
        }

        /// <summary>
        /// Moves by flying.
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

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public override IReproducer Reproduce()
        {
            // Lay an egg.
            IReproducer baby = this.LayEgg();

            // If the baby is hatchable...
            if (baby is IHatchable)
            {
                // Hatch the baby out of its egg.
                this.HatchEgg(baby as IHatchable);
            }

            // Return the (hatched) baby.
            return baby;
        }

        /// <summary>
        /// Hatches an egg.
        /// </summary>
        /// <param name="egg">The egg to hatch.</param>
        private void HatchEgg(IHatchable egg)
        {
            // Hatch the egg.
            egg.Hatch();
        }

        /// <summary>
        /// Lays an egg.
        /// </summary>
        /// <returns>The resulting egg.</returns>
        private IReproducer LayEgg()
        {
            // Return a baby (in egg form).
            return base.Reproduce();
        }
    }
}