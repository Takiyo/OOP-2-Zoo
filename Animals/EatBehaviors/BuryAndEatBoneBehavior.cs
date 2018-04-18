using Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class used to represent burying and eating a bone.
    /// </summary>
    [Serializable]
    public class BuryAndEatBoneBehavior : IEatBehavior
    {
        public void Eat(IEater eater, Food food)
        {
            // Eater buries bone.
            this.BuryBone(food);
            // Eater digs up the bone.
            this.DigUpAndEatBone();
            // Eater eats the bone.
            eater.Eat(food);
            // Eater barks in satisfaction.
            this.Bark();
        }

        /// <summary>
        /// Barks in satisfaction after eating.
        /// </summary>
        private void Bark()
        {

        }

        /// <summary>
        /// Buries bone.
        /// </summary>
        /// <param name="bone">The bone to be buried.</param>
        private void BuryBone(Food bone)
        {

        }

        /// <summary>
        /// Digs up bone.
        /// </summary>
        private void DigUpAndEatBone()
        {

        }
    }
}
