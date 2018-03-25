using System;
using System.Collections.Generic;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The zoo's list of cages.
        /// </summary>
        private List<Cage> cages = new List<Cage>();

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's information booth.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="boothMoneyBalance">The money balance of the money collecting booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>()
            {
                    new Chimpanzee("Bobo", 10, 128.2, Gender.Male),
                    new Chimpanzee("Bubbles", 3,  103.8, Gender.Female),
                    new Dingo("Spot", 5, 41.3, Gender.Male),
                    new Dingo("Maggie", 6, 37.2, Gender.Female),
                    new Dingo("Toby", 0, 15.0, Gender.Male),
                    new Eagle("Ari", 12, 10.1, Gender.Female),
                    new Hummingbird("Buzz", 2, 0.02, Gender.Male),
                    new Hummingbird("Bitsy", 1, 0.03, Gender.Female),
                    new Kangaroo("Kanga", 8, 72.0, Gender.Female),
                    new Kangaroo("Roo", 0, 23.9, Gender.Male),
                    new Kangaroo("Jake", 9,153.5, Gender.Male),
                    new Ostrich("Stretch", 26, 231.7, Gender.Male),
                    new Ostrich("Speedy", 30, 213.0, Gender.Female),
                    new Platypus("Patti", 13, 4.4, Gender.Female),
                    new Platypus("Bill", 11, 4.9, Gender.Male),
                    new Platypus("Ted", 0, 1.1, Gender.Male),
                    new Shark("Bruce", 19,  810.6, Gender.Female),
                    new Shark("Anchor", 17, 458.0, Gender.Male),
                    new Shark("Chum", 14, 377.3, Gender.Male),
                    new Squirrel("Chip", 4, 1.0, Gender.Male),
                    new Squirrel("Dale", 4, 0.9, Gender.Male)
            };
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.informationBooth = new GivingBooth(attendant);
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());
            this.ticketBooth.AddMoney(boothMoneyBalance);
            
            foreach (AnimalType at in Enum.GetValues(typeof(AnimalType)))
            {
                Cage cage = new Cage(400, 800, Animal.ConvertAnimalTypeToType(at));
                cages.Add(cage);
            }
        }

        /// <summary>
        /// Gets a list of the zoo's animals.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets the zoo's animal snack machine.
        /// </summary>
        public VendingMachine AnimalSnackMachine
        {
            get
            {
                return this.animalSnackMachine;
            }
        }

        /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets a list of the zoo's guests.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                double totalWeight = 0;

                // Loop through the zoo's list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add the current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>A newly created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

            // Set the initial money balance of the animal snack machine.
            comoZoo.AnimalSnackMachine.AddMoney(42.75m);    

            return comoZoo;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);
            Cage cage = this.FindCage(animal.GetType());
            cage.Add(animal);
            if (animal.IsPregnant == true)
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            }
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket == null || ticket.IsRedeemed == true)
            {
                throw new NullReferenceException("Guest " + guest.Name + " was not added because they did not have a ticket.");
            }
            else
            {
                ticket.Redeem();
                this.guests.Add(guest);
            }
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        public void BirthAnimal(IReproducer reproducer)
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal(reproducer);

            // If the baby is an animal...
            if (baby is Animal)
            {
                // Add the baby to the zoo's list of animals.
                this.AddAnimal(baby as Animal);
            }
        }

        /// <summary>
        /// Find an animal based on type.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(Type type)
        {
            Animal animal = null;

            // Loop through the zoo's list of animals.
            foreach (Animal a in this.animals)
            {
                // If the type matches...
                if (a.GetType() == type)
                {
                    // Get the matching animal.
                    animal = a;

                    break;
                }
            }

            return animal;
        }

        /// <summary>
        /// Finds an animal based on type and pregnancy status.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <param name="isPregnant">The pregnancy status of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(Type type, bool isPregnant)
        {
            Animal animal = null;

            // Loop through the zoo's list of animals.
            foreach (Animal a in this.animals)
            {
                // If the type and pregnancy status match...
                if (a.GetType() == type && a.IsPregnant == isPregnant)
                {
                    // Get the matching animal.
                    animal = a;

                    break;
                }
            }

            return animal;
        }

        /// <summary>
        /// Finds the first animal with the specified name.
        /// </summary>
        /// <param name="name">The name of the animal to find.</param>
        /// <returns>The animal that was found.</returns>
        public Animal FindAnimal(string name)
        {
            // Define and initialize a result variable.
            Animal result = null;

            // Loop through all animals in the zoo.
            foreach (Animal a in this.animals)
            {
                // If the desired animal was found...
                if (a.Name == name)
                {
                    // Set the variable to point to the current aniimal.
                    result = a;

                    // Break out of the loop (no need to continue looking).
                    break;
                }
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// Finds the cage based on animal type.
        /// </summary>
        /// <param name="animalType">The type of cage to be searched.</param>
        /// <returns>The found cage.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage cage = null;

            // Loop through the zoo's list of cages.
            foreach (Cage c in this.cages)
            {
                // Checks cage animal type against passed in animal type.
                if(c.AnimalType == animalType)
                {
                    cage = c;
                    break;
                }
            }

            return cage;
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="name">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public Guest FindGuest(string name)
        {
            Guest guest = null;

            // Loop through the zoo's list of guests.
            foreach (Guest g in this.guests)
            {
                // If the name matches...
                if (g.Name == name)
                {
                    // Get the matching guest.
                    guest = g;

                    break;
                }
            }

            return guest;
        }

        /// <summary>
        /// Gets a list of animals by type from the zoo's list of animals.
        /// </summary>
        /// <param name="type">The type to be searched.</param>
        /// <returns>The found animals.</returns>
        public IEnumerable<Animal> GetAnimals(Type type)
        {
            // Creates an instantiates a list for found animals.
            List<Animal> resultlist = new List<Animal>();

            // Loops through each animal and checks for type.
            foreach(Animal a in this.Animals)
            {
                if (a.GetType() == type)
                {
                    // Adds matching type to list.
                    resultlist.Add(a);
                    break;
                }
            }

            return resultlist;
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="animal">The animal to remove.</param>
        public void RemoveAnimal(Animal animal)
        {
            this.animals.Remove(animal);
            Cage cage = this.FindCage(animal.GetType());
            cage.Remove(animal);
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="guest">The guest to remove.</param>
        public void RemoveGuest(Guest guest)
        {
            this.guests.Remove(guest);
        }

        /// <summary>
        /// Sells a ticket to a guest.
        /// </summary>
        /// <param name="guest">The guest to whom to sell the ticket.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            // Buy a ticket and a water bottle.
            Ticket ticket = guest.VisitTicketBooth(this.ticketBooth);

            // Get a coupon book and a map.
            guest.VisitInformationBooth(this.informationBooth);

            return ticket;
        }

        /// <summary>
        /// Sorts zoo animals.
        /// </summary>
        /// <param name="sortType">The type of sorting algorithm to be used.</param>
        /// <param name="sortValue">The value to be sorted with.</param>
        /// <returns></returns>
        public SortResult SortAnimals(string sortType, string sortValue)
        {
            SortResult result = null;

            switch (sortType)
            {
                case "bubble":
                    if (sortValue == "weight")
                    {
                        result = SortHelper.BubbleSortByWeight(this.animals);
                    }
                    if (sortValue == "name")
                    {
                        result = SortHelper.BubbleSortByName(this.animals);
                    }
                    break;
                case "selection":
                    if (sortValue == "weight")
                    {
                        result = SortHelper.SelectionSortByWeight(this.animals);
                    }
                    if (sortValue == "name")
                    {
                        result = SortHelper.SelectionSortByName(this.animals);
                    }
                    break;

                case "insertion":
                    if (sortValue == "weight")
                    {
                        result = SortHelper.InsertionSortByWeight(this.animals);
                    }
                    if (sortValue == "name")
                    {
                        result = SortHelper.InsertionSortByName(this.animals);
                    }
                    break;
            }

            return result;
        }
    }
}