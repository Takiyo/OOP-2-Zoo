﻿using Reproducers;

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
            // Swim.
        }
    }
}