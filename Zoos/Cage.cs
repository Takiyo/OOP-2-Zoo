using Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Zoos
{
    /// <summary>
    /// The class used to represent an animal cage.
    /// </summary>
    public class Cage
    {
        /// <summary>
        /// The zoo's list of animals.
        /// </summary>
        private List<ICageable> cagedItems = new List<ICageable>();

        /// <summary>
        /// Initializes a new instance of the Cage class.
        /// </summary>
        /// <param name="height">The cage's height.</param>
        /// <param name="width">The cage's width.</param>
        /// <param name="animalType">The type of animal the cage holds.</param>
        public Cage(int height, int width, Type animalType)
        {
            this.Height = height;
            this.Width = width;
            this.AnimalType = animalType;
        }

        /// <summary>
        /// Gets or sets the cage's animal type.
        /// </summary>
        public Type AnimalType { get; private set; }

        /// <summary>
        /// Gets or sets the cage's height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the cage's width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets the list of animals.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        /// <summary>
        /// Adds animal to cage.
        /// </summary>
        /// <param name="animal">Animal to be added.</param>
        public void Add(ICageable cagedItem)
        {
            this.cagedItems.Add(cagedItem);
        }

        /// <summary>
        /// Removes animal from cage.
        /// </summary>
        /// <param name="animal">Animal to be removed.</param>
        public void Remove(ICageable cagedItem)
        {
            this.cagedItems.Remove(cagedItem);
        }


        public override string ToString()
        {
            string result = $"{AnimalType.Name} cage ({this.Width}x{this.Height})";

            foreach (Animal a in CagedItems)
            {
                result += $"{Environment.NewLine}{a.ToString()} ({a.XPosition}x{a.YPosition})";
            }
            return result;
        }

    }
}
