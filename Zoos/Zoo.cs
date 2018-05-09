using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;
using System.Runtime.Serialization;
using System.Collections;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperatureChange;

        [NonSerialized]
        private Action<Guest> onAddGuest;

        [NonSerialized]
        private Action<Guest> onRemoveGuest;

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
        private Dictionary<Type, Cage> cages = new Dictionary<Type, Cage>();

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
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.b168.OnTemperatureChange += this.HandleBirthingRoomTemperatureChange;
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
                Cage cage = new Cage(400, 800);
                cages.Add(Animal.ConvertAnimalTypeToType(at), cage);
            }

            // Animals for sorting
            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));
            this.AddAnimal(new Shark("Chum", 14, 377.3, Gender.Male));
            this.AddAnimal(new Squirrel("Chip", 4, 1.0, Gender.Male));
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            // Guests for sorting
            this.AddGuest(new Guest("Greg", 35, 100.0m, WalletColor.Crimson, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Darla", 7, 10.0m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Anna", 8, 12.56m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Matthew", 42, 10.0m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Doug", 7, 11.10m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Jared", 17, 31.70m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sean", 34, 20.50m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sally", 52, 134.20m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));

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
        /// Gets or sets the onbirthingroomtemperaturechange delegate.
        /// </summary>
        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperatureChange;
            }
            set
            {
                this.onBirthingRoomTemperatureChange = value;
            }
        }

        /// <summary>
        /// Handles the birthing room temp change.
        /// </summary>
        /// <param name="previousTemp">The temp the birthing room was previously.</param>
        /// <param name="currentTemp">The temp the birthing room is currently.</param>
        /// <param name="currentTemp"></param>
        private void HandleBirthingRoomTemperatureChange(double previousTemp, double currentTemp)
        {
            if (this.OnBirthingRoomTemperatureChange != null)
            {
                this.OnBirthingRoomTemperatureChange(previousTemp, currentTemp);
            }
        }

        /// <summary>
        /// When the guest is added action.
        /// </summary>
        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }
            set
            {
                this.onAddGuest = value;
            }
        }

        /// <summary>
        /// When the guest is removed action.
        /// </summary>
        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }
            set
            {
                this.onRemoveGuest = value;
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
            animal.IsActive = true;

            this.animals.Add(animal);
            Cage cage = this.FindCage(animal.GetType());
            cage = cages[animal.GetType()];
            cage.Add(animal);
            if (animal.IsPregnant == true)
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            }
        }

        /// <summary>
        /// Adds animals to the zoo.
        /// </summary>
        /// <param name="animals">The list of animals to be added to.</param>
        public void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            foreach (Animal a in animals)
            {
                this.AddAnimal(a);

                // using recursion, add the current animal's children to the zoo
                AddAnimalsToZoo(a.Children);
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
                // Guest has access to vending machine.
                guest.GetVendingMachine += this.ProvideVendingMachine;

                // Guest's ticket is redeemed.
                ticket.Redeem();

                // Guest added to zoo.
                this.guests.Add(guest);

                if (this.OnAddGuest != null)
                {
                    this.OnAddGuest(guest);
                }
            }
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        public void BirthAnimal()
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal();

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
            Cage result = null;
            this.cages.TryGetValue(animalType, out result);
            return result;
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
            animal.IsActive = false;

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
            guest.IsActive = false;

            this.guests.Remove(guest);
            if (this.OnRemoveGuest != null)
            {
                this.OnRemoveGuest(guest);
            }
        }

        /// <summary>
        /// Saves the zoo to a file.
        /// </summary>
        /// <param name="fileName">The file to be saved.</param>
        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of the zoo
                formatter.Serialize(stream, this);
            }
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
        /// <returns>A sorted list.</returns>
        private SortResult SortObjects(string sortType, string sortValue, IList list)
        {
            Func<object, object, int> sortFunc;

            if (sortValue == "animalName")
            {
                sortFunc = AnimalNameSortComparer;
            }
            else if (sortValue == "guestName")
            {
                sortFunc = GuestNameSortComparer;
            }
            else if (sortValue == "weight")
            {
                sortFunc = WeightSortComparer;
            }
            else if (sortValue == "age")
            {
                sortFunc = AgeSortComparer;
            }
            else
            {
                sortFunc = MoneyBalanceSortComparer;
            }

            SortResult result = new SortResult();

            switch (sortType)
            {
                case "bubble":
                        result = SortHelper.BubbleSort(list, sortFunc);
                    break;
                case "selection":
                        result = SortHelper.SelectionSort(list, sortFunc);
                    break;

                case "insertion":
                        result = SortHelper.InsertionSort(list, sortFunc);
                    break;

                case "quick":
                    if (sortValue == "weight")
                        result = SortHelper.QuickSort(list, 0, list.Count - 1, result, sortFunc);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Sorts zoo guests.
        /// </summary>
        /// <param name="sortType">The type of sorting algorithm to be used.</param>
        /// <param name="sortValue">The value to be sorted with.</param>
        /// <returns>A sorted list.</returns>
        public SortResult SortGuests(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.guests);
        }

        /// <summary>
        /// Sorts zoo guests.
        /// </summary>
        /// <param name="sortType">The type of sorting algorithm to be used.</param>
        /// <param name="sortValue">The value to be sorted with.</param>
        /// <returns>A sorted list.</returns>
        public SortResult SortAnimals(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.animals);
        }

        /// <summary>
        /// Loads a zoo from the passed-in file.
        /// </summary>
        /// <param name="fileName">The file name to be loaded.</param>
        /// <returns>The zoo from the file.</returns>
        public static Zoo LoadFromFile(string fileName)
        {
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            return result;
        }

        /// <summary>
        /// Provides a vending machine so animals can be fed.
        /// </summary>
        /// <returns></returns>
        private VendingMachine ProvideVendingMachine()
        {
            return this.AnimalSnackMachine;
        }

        /// <summary>
        /// Sets the delegate.
        /// </summary>
        public void OnDeserialized()
        {
            foreach (Guest g in this.Guests)
            {
                if (this.OnAddGuest != null)
                {
                    this.OnAddGuest(g);
                }
            }
            //placeholder for animals
            if (this.OnBirthingRoomTemperatureChange != null)
            {
                this.OnBirthingRoomTemperatureChange(this.b168.Temperature, this.b168.Temperature);
            }
        }

        /// <summary>
        /// Compares animals by name.
        /// </summary>
        /// <param name="object1">First object to be compared.</param>
        /// <param name="object2">Second object to be compared.</param>
        /// <returns>The result of the comparison.</returns>
        private static int AnimalNameSortComparer(Object object1, Object object2)
        {
            if (object1 is Animal)
            {
                return string.Compare((object1 as Animal).Name, (object2 as Animal).Name);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// Compares guests by name.
        /// </summary>
        /// <param name="object1">First object to be compared.</param>
        /// <param name="object2">Second object to be compared.</param>
        /// <returns>The result of the comparison.</returns>
        private static int GuestNameSortComparer(Object object1, Object object2)
        {
            if (object1 is Guest)
            {
                return string.Compare((object1 as Guest).Name, (object2 as Guest).Name);
            }
            else
            {
                return 0;
            }
        }

            /// <summary>
            /// Compares objects by weight.
            /// </summary>
            /// <param name="object1">First object to be compared.</param>
            /// <param name="object2">Second object to be compared.</param>
            /// <returns>The result of the comparison.</returns>
            private static int WeightSortComparer(Object object1, Object object2)
        {
            if (object1 is Animal)
            {
                if ((object1 as Animal).Weight == (object2 as Animal).Weight)
                {
                    return 0;
                }
                else if ((object1 as Animal).Weight > (object2 as Animal).Weight)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            if (object1 is Guest)
            {
                if ((object1 as Guest).Weight == (object2 as Guest).Weight)
                {
                    return 0;
                }
                else if ((object1 as Guest).Weight > (object2 as Guest).Weight)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Compares objects by money balance.
        /// </summary>
        /// <param name="object1">First object to be compared.</param>
        /// <param name="object2">Second object to be compared.</param>
        /// <returns>The result of the comparison.</returns>
        private static int MoneyBalanceSortComparer(Object object1, Object object2)
        {
            if (object1 is Guest)
            {
                if ((object1 as Guest).CheckingAccount.MoneyBalance + (object1 as Guest).Wallet.MoneyBalance
                    == (object2 as Guest).CheckingAccount.MoneyBalance + (object2 as Guest).Wallet.MoneyBalance)
                {
                    return 0;
                }
                else if ((object1 as Guest).CheckingAccount.MoneyBalance + (object1 as Guest).Wallet.MoneyBalance
                    > (object2 as Guest).CheckingAccount.MoneyBalance + (object2 as Guest).Wallet.MoneyBalance)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Compares objects by age.
        /// </summary>
        /// <param name="object1">First object to be compared.</param>
        /// <param name="object2">Second object to be compared.</param>
        /// <returns>The result of the comparison.</returns>
        private static int AgeSortComparer(Object object1, Object object2)
        {
            if (object1 is Animal)
            {
                if ((object1 as Animal).Age == (object2 as Animal).Age)
                {
                    return 0;
                }
                else if ((object1 as Animal).Age > (object2 as Animal).Age)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            if (object1 is Guest)
            {
                if ((object1 as Guest).Age == (object2 as Guest).Age)
                {
                    return 0;
                }
                else if ((object1 as Guest).Age > (object2 as Guest).Age)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            return 0;
        }

        }
    }