using Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class used to show affection.
    /// </summary>
    public class ShowAffectionBehavior : IEatBehavior
    {
        public void Eat(IEater eater, Food food)
        {
            eater.Eat(food);
            this.ShowAffection();
        }

        /// <summary>
        /// The eater shows affection.
        /// </summary>
        private void ShowAffection()
        {
        }
    }
}
