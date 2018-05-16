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
        public static IEnumerable<object> GetYoungGuests(this Zoo zoo)
        {
            //List<Guest> youngGuests = new List<Guest>();
            //string firstYoung = "";

            //youngGuests.Add(zoo.Guests.ToList().FirstOrDefault(g => g.Age <= 10));
            //youngGuests.ForEach(g => firstYoung = g.ToString());
            //return $"First young guest found: {firstYoung}";

            IEnumerable<object> youngGuests = from o in zoo.Guests
                                              where o.Age <= 10
                                              select new
                                              {
                                                  o.Name,
                                                  o.Age
                                              };
            return youngGuests;
        }

        /// <summary>
        /// Gets female dingoes from the zoo's list of animals.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of female dingoes.</returns>
        public static IEnumerable<object> GetFemaleDingoes(this Zoo zoo)
        {
            IEnumerable<object> femaleDingoes = from o in zoo.Animals
                                              where o.Gender == Gender.Female && o.GetType() == typeof(Dingo)
                                              select new
                                              {
                                                  o.Name,
                                                  o.Age,
                                                  o.Weight,
                                                  o.Gender
                                              };
            return femaleDingoes;
        }

        /// <summary>
        /// Gets heavy animals from the zoo's list of animals.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of heavy animals</returns>
        public static IEnumerable<object> GetHeavyAnimals(this Zoo zoo)
        {
            IEnumerable<object> heavyAnimals = from o in zoo.Animals
                                               where o.Weight > 200
                                               select new
                                               {
                                                  //o.GetType().Name,
                                                   o.Name,
                                                   o.Age,
                                                   o.Weight
                                               };

            return heavyAnimals;
        }

        /// <summary>
        /// Gets guests sorted by age.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of sorted guests.</returns>
        public static IEnumerable<object> GetGuestsByAge(this Zoo zoo)
        {
            IEnumerable<object> guestsYoungToOld = from o in zoo.Guests.OrderBy(g => g.Age)
                                                   where true
                                                   select new
                                                   {
                                                       o.Name,
                                                       o.Age,
                                                       o.Gender
                                                   };
            return guestsYoungToOld;
        }

        /// <summary>
        /// Gets all the flying animals in the zoo.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of flying animals.</returns>
        public static IEnumerable<object> GetFlyingAnimals(this Zoo zoo)
        {
            IEnumerable<object> flyingAnimals = from o in zoo.Animals
                                                where o.MoveBehavior is FlyBehavior
                                                select new
                                                {
                                                    Type = o.GetType(),
                                                    o.Name
                                                };

            return flyingAnimals;
        }

        /// <summary>
        /// Gets all guests that have an adopted animal.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of guests and adopted animals.</returns>
        public static IEnumerable<object> GetAdoptedAnimals(this Zoo zoo)
        {
            IEnumerable<object> adopters = from o in zoo.Guests
                                           where o.AdoptedAnimal != null
                                                select new
                                                {
                                                    Adopter = o.Name,
                                                    o.AdoptedAnimal.Name,
                                                    Type = o.AdoptedAnimal.GetType()
                                                };

            return adopters;
        }

        /// <summary>
        /// Gets the guests grouped and ordered by their wallet color.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of guests ordered by their wallet color.</returns>
        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo zoo)
        {
            IEnumerable<object> moneyBalanceByWalletColor = from o in zoo.Guests.OrderBy(g => g.Wallet.Color)
                                                      from g in zoo.Guests.GroupBy(g => g.Wallet.Color)
                                                      where true
                                                      select new
                                                      {
                                                          g.Key,
                                                          TotalMoneyBalance = g.Sum(o => o.Wallet.MoneyBalance)
                                                      };

            return moneyBalanceByWalletColor;
        }

        /// <summary>
        /// Gets the animals grouped and ordered by their types.
        /// </summary>
        /// <param name="zoo">The zoo to be searched.</param>
        /// <returns>A list of animals ordered by their type.</returns>
        public static IEnumerable<object> GetAverageWeightByAnimalType(this Zoo zoo)
        {
            IEnumerable<object> averageWeightByType = from o in zoo.Animals.OrderBy(a => a.GetType())
                                                      from a in zoo.Animals.GroupBy(a => a.GetType())
                                                      where true
                                                      select new
                                                      {
                                                          a.Key,
                                                          AverageWeight = a.Average(o => o.Weight)
                                                      };

            return averageWeightByType;
        }
    }
}
