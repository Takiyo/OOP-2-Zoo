using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a platypus.
    /// </summary>
    public sealed class Platypus : Mammal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Platypus class.
        /// </summary>
        /// <param name="name">The name of the platypus.</param>
        /// <param name="age">The age of the platypus.</param>
        /// <param name="weight">The weight of the platypus (in pounds).</param>
        /// <param name="gender">The gender of the platypus.</param>
        public Platypus(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 12.0;
        }

        /// <summary>
        /// The platypus's display size for the cage.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                double result = (this.Age == 0) ? 0.8 : 1.1;
                return result;
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public override void Eat(Food food)
        {
            this.StashInPouch(food);

            base.Eat(food);
        }

        /// <summary>
        /// Hatches the platypus.
        /// </summary>
        public void Hatch()
        {
            // The platypus hatches from an egg.
        }

        /// <summary>
        /// Moves by swimming.
        /// </summary>
        public override void Move()
        {
            MoveHelper.Swim(this);
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public override IReproducer Reproduce()
        {
            // Lay an egg.
            IReproducer result = this.LayEgg();

            // If the baby is hatchable...
            if (result is IHatchable)
            {
                // Hatch the baby (egg).
                this.HatchEgg(result as IHatchable);
            }

            // Return the (hatched) baby.
            return result;
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
            // Return the baby (egg) from the base Reproduce method.
            return base.Reproduce();
        }

        /// <summary>
        /// Stashes food in its cheek pouches.
        /// </summary>
        /// <param name="food">The food to be stashed.</param>
        private void StashInPouch(Food food)
        {
            // Stash food to eat later.
        }
    }
}