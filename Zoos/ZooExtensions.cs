using Animals;
using People;
using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    /// <summary>
    /// The class used to represent extensions on the zoo class.
    /// </summary>
    public static class ZooExtensions
    {
        /// <summary>
        /// Find an animal based on type.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public static Animal FindAnimal(this Zoo zoo, Predicate<Animal> match) => zoo.Animals.ToList().Find(match);

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="name">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo zoo, Predicate<Guest> match) => zoo.Guests.ToList().Find(match);

        /// <summary>
        /// Gets young guests from the zoo's list of guests.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of young guests.</returns>
        public static IEnumerable<object> GetYoungGuests(Zoo zoo)
        {
            //List<Guest> youngGuests = new List<Guest>();
            //string firstYoung = "";

            //youngGuests.Add(zoo.Guests.ToList().FirstOrDefault(g => g.Age <= 10));
            //youngGuests.ForEach(g => firstYoung = g.ToString());
            //return $"First young guest found: {firstYoung}";

            IEnumerable<object> youngGuests = from o in zoo.Guests where o.Age <= 10 select o;
            return youngGuests;
        }

        /// <summary>
        /// Gets female dingoes from the zoo's list of animals.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of female dingoes.</returns>
        public static IEnumerable<object> GetFemaleDingoes(Zoo zoo)
        {
            IEnumerable<object> youngGuests = from o in zoo.Animals where o.Gender == Gender.Female && o.GetType == typeof(Dingo) select o;
        }

        /// <summary>
        /// Gets heavy animals from the zoo's list of animals.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of heavy animals</returns>
        public static IEnumerable<object> GetHeavyAnimals(Zoo zoo)
        {

        }
    }
}
