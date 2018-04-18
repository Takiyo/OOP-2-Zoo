﻿using Reproducers;
using System;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a shark.
    /// </summary>
    [Serializable]
    public class Shark : Fish
    {
        /// <summary>
        /// Initializes a new instance of the Shark class.
        /// </summary>
        /// <param name="name">The name of the shark.</param>
        /// <param name="age">The age of the shark.</param>
        /// <param name="weight">The weight of the shark.</param>
        /// <param name="gender">The gender of the shark.</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 18.0;
        }

        /// <summary>
        /// The shark's display size for the cage.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                
                double result = (this.Age == 0) ? 1 : 1.5;
                return result;
            }
        }
    }
}