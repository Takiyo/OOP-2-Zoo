using System;
using System.Collections.Generic;
using System.Linq;
using Accounts;
using Animals;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class that provides console functionality.
    /// </summary>
    internal static class ConsoleHelper
    {
        /// <summary>
        /// Saves the zoo to a file.
        /// </summary>
        /// <param name="zoo">The zoo to be saved.</param>
        /// <param name="fileName">The file saved.</param>
        public static void SaveFile(Zoo zoo, string fileName)
        {
            try
            {
                zoo.SaveToFile(fileName);

                Console.WriteLine("Your zoo has been successfully saved.");
            }
            catch (Exception)
            {
                Console.WriteLine("The save was unsuccessful.");
            }
        }

        /// <summary>
        /// Sets the birthing room temperature of a zoo.
        /// </summary>
        /// <param name="zoo">The zoo to update.</param>
        /// <param name="temperature">The value to which to set the temperature.</param>
        public static void SetTemperature(Zoo zoo, string temperature)
        {
            try
            {
                double newTemp = double.Parse(temperature);
                double previousTemp = zoo.BirthingRoomTemperature;
                zoo.BirthingRoomTemperature = newTemp;

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Temperature must be a number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Processes the add console command.
        /// </summary>
        /// <param name="zoo">The zoo to contain the added object.</param>
        /// <param name="type">The type of object to add.</param>
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    ConsoleHelper.AddAnimal(zoo);

                    break;
                case "guest":
                    ConsoleHelper.AddGuest(zoo);

                    break;
                default:
                    Console.WriteLine("Unknown type. Only animals and guests can be added.");

                    break;
            }
        }

        /// <summary>
        /// Processes the show console command.
        /// </summary>
        /// <param name="zoo">The zoo containing the object to show.</param>
        /// <param name="type">The type of object to show.</param>
        /// <param name="name">The name of the object to show.</param>
        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            string uppercaseName = ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ConsoleHelper.RemoveAnimal(zoo, uppercaseName);

                    break;
                case "guest":
                    ConsoleHelper.RemoveGuest(zoo, uppercaseName);

                    break;
                default:
                    Console.WriteLine("Unknown type. Only animals and guests can be removed.");

                    break;
            }
        }

        /// <summary>
        /// Processes the show console command.
        /// </summary>
        /// <param name="zoo">The zoo containing the object to show.</param>
        /// <param name="type">The type of object to show.</param>
        /// <param name="name">The name of the object to show.</param>
        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            string uppercaseName = ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ConsoleHelper.ShowAnimal(zoo, uppercaseName);

                    break;
                case "guest":
                    ConsoleHelper.ShowGuest(zoo, uppercaseName);

                    break;
                case "cage":
                    ConsoleHelper.ShowCage(zoo, uppercaseName);
                    
                    break;
                case "children":
                    ConsoleHelper.ShowChildren(zoo, uppercaseName);
                    break;
                default:
                    Console.WriteLine("Unknown type. Only animals and guests can be shown.");

                    break;
            }
        }

        /// <summary>
        /// Shows an animal in the console.
        /// </summary>
        /// <param name="zoo">The zoo in which the animal resides.</param>
        /// <param name="name">The name of the animal to show.</param>
        private static void ShowAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);

            if (animal != null)
            {
                Console.WriteLine($"The following animal was found: {animal}.");
            }
            else
            {
                Console.WriteLine("Animal could not be found.");
            }
        }

        /// <summary>
        /// Shows the children of the passed in animal.
        /// </summary>
        /// <param name="zoo">The zoo that houses the animals.</param>
        /// <param name="name">The name of the animal's children being shown.</param>
        private static void ShowChildren(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);
            ConsoleHelper.WalkTree(animal, "");
        }

        /// <summary>
        /// Shows a guest in the console.
        /// </summary>
        /// <param name="zoo">The zoo in which the guest resides.</param>
        /// <param name="name">The name of the guest to show.</param>
        private static void ShowGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(g => g.Name == name);

            if (guest != null)
            {
                Console.WriteLine($"The following guest was found: {guest}.");
            }
            else
            {
                Console.WriteLine("Guest could not be found.");
            }
        }

        /// <summary>
        /// Shows details about help command.
        /// </summary>
        /// <param name="command">The command entered.</param>
        public static void ShowHelpDetail(string command)
        {
            Dictionary<string, string> arguments;
            switch (command)
            {
                case "show":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to show (ANIMAL, GUEST, or CAGE).");
                    arguments.Add("objectName", "The name of the object to show (use an animal name for CAGE).");
                    ConsoleUtil.WriteHelpDetail(command, "Shows details of an object.", arguments);
                    break;
                case "remove":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to remove (ANIMAL, or GUEST).");
                    arguments.Add("objectName", "The name of the object to remove.");
                    ConsoleUtil.WriteHelpDetail(command, "Removes an object from the zoo.", arguments);
                    break;
                case "temp":
                    ConsoleUtil.WriteHelpDetail(command, "Changes the temperature in the zoo's birthing room.", "temperature",
                        "The temperature you wish to change the birthing room to, in fahrenheit.");
                    break;
                case "add":
                    ConsoleUtil.WriteHelpDetail(command, "Adds either a guest or an animal to the zoo.", "objectType",
                        "The type of object to add (ANIMAL or GUEST).");
                    break;
                case "restart":
                    ConsoleUtil.WriteHelpDetail(command, "Creates a new como zoo and erases the old one.");
                    break;
                case "exit":
                    ConsoleUtil.WriteHelpDetail(command, "Exits the application.");
                    break;
            }
        }

        /// <summary>
        /// Shows help for commands with no parameters.
        /// </summary>
        public static void ShowHelp()
        {
            ConsoleUtil.WriteHelpDetail("help", "Show help detail", "[command]", "The (optional) command for which to show help details.");
            Console.WriteLine("Known commands:");
            Console.WriteLine("HELP: Shows a list of known commands.");
            Console.WriteLine("EXIT: Exits the application.");
            Console.WriteLine("RESTART: Creates a new zoo.");
            Console.WriteLine("TEMPERATURE: Sets the birthing room temperature.");
            Console.WriteLine("SHOW ANIMAL [animal name]: Displays information for specified animal.");
            Console.WriteLine("SHOW GUEST [guest name]: Displays information for specified guest.");
            Console.WriteLine("ADD: Adds an animal or guest to the zoo.");
            Console.WriteLine("REMOVE: Removes an animal or guest from the zoo.");
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the animal to remove.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animalToRemove = zoo.FindAnimal(a => a.Name == name);

            try
            {
                zoo.RemoveAnimal(animalToRemove);
                Console.WriteLine(animalToRemove.Name + " was removed from the zoo.");
            }
            catch
            {
                Console.WriteLine("The animal could not be removed.");
            }
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the guest to remove.</param>
        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guestToRemove = zoo.FindGuest(a => a.Name == name);

            try
            {
                zoo.RemoveGuest(guestToRemove);
                Console.WriteLine(guestToRemove.Name + " was removed from the zoo.");
            }
            catch
            {
                Console.WriteLine("The guest could not be removed.");
            }
        }

        /// <summary>
        /// Adds a new animal to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        private static void AddAnimal(Zoo zoo)
        {
            AnimalType animalType = ConsoleUtil.ReadAnimalType();

            Animal animal = AnimalFactory.CreateAnimal(animalType, string.Empty, 0, 1, Gender.Female);

            if (animal == null)
            {
                throw new NullReferenceException("Animal could not be found.");
            }

            animal.Name = ConsoleUtil.InitialUpper(ConsoleUtil.ReadAlphabeticValue("Name"));
            animal.Gender = ConsoleUtil.ReadGender();
            animal.Age = ConsoleUtil.ReadIntValue("Age");
            animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");

            zoo.AddAnimal(animal);

            ConsoleHelper.ShowAnimal(zoo, animal.Name);
        }

        /// <summary>
        /// Adds a new guest to the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        private static void AddGuest(Zoo zoo)
        {
            bool success = false;

            Guest guest = new Guest(string.Empty, 0,  0m, WalletColor.Black, Gender.Female, new Account());

            if (guest == null)
            {
                throw new NullReferenceException("Guest could not be found.");
            }

            while (!success)
            {
                try
                {
                    string name = ConsoleUtil.ReadAlphabeticValue("Name");

                    guest.Name = ConsoleUtil.InitialUpper(name);
                    success = true;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Gender = ConsoleUtil.ReadGender();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    int age = ConsoleUtil.ReadIntValue("Age");

                    guest.Age = age;
                    success = true;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    double moneyBalance = ConsoleUtil.ReadDoubleValue("Wallet money balance");

                    guest.Wallet.AddMoney((decimal)moneyBalance);
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    guest.Wallet.Color = ConsoleUtil.ReadWalletColor();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    double moneyBalance = ConsoleUtil.ReadDoubleValue("Checking account money balance");

                    guest.CheckingAccount.AddMoney((decimal)moneyBalance);
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Ticket ticket = zoo.SellTicket(guest);

            zoo.AddGuest(guest, ticket);

            ConsoleHelper.ShowGuest(zoo, guest.Name);
        }

        /// <summary>
        /// Loads a file.
        /// </summary>
        /// <param name="fileName">The file name to be loaded.</param>
        /// <returns>The loaded zoo.</returns>
        public static Zoo LoadFile(string fileName)
        {
            try
            {
                Zoo zoo = Zoo.LoadFromFile(fileName);

                Console.WriteLine("Your zoo has been loaded successfully.");
                return zoo;
            }
            catch (Exception)
            {
                Console.WriteLine("The load was unsuccessful");
                return null;
            }
        }

        /// <summary>
        /// Shows the cage in the console.
        /// </summary>
        /// <param name="zoo">The zoo whose cage is to be shown.</param>
        /// <param name="animalName">The animal in the cage.</param>
        private static void ShowCage(Zoo zoo, string animalName)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == animalName);

            if (animal != null)
            {
                Cage cage = zoo.FindCage(animal.GetType());

                if (cage != null)
                {
                    Console.WriteLine("Cage found: " + cage.ToString());
                }
            }
            else
            {
                Console.WriteLine("Animal could not be found.");
            }
        }

        /// <summary>
        /// placeholder
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="prefix"></param>
        private static void WalkTree(Animal animal, string prefix)
        {
            Console.WriteLine(prefix + animal.ToString());
            foreach (Animal a in animal.Children)
            {
                ConsoleHelper.WalkTree(a, prefix + "  ");
            }
        }

        /// <summary>
        /// Attaches delegates to the zoo.
        /// </summary>
        /// <param name="zoo">The zoo being changed.</param>
        public static void AttachDelegates(Zoo zoo)
        {
             zoo.OnBirthingRoomTemperatureChange += ConsoleHelper.HandleBirthingRoomTemperatureChange;
        }

        /// <summary>
        /// Handles the birthing room temp change.
        /// </summary>
        /// <param name="previousTemp">The temp the birthing room was previously.</param>
        /// <param name="currentTemp">The temp the birthing room is currently.</param>
        private static void HandleBirthingRoomTemperatureChange(double previousTemp, double currentTemp)
        {
            Console.WriteLine($"Previous temperature: {previousTemp}");
            Console.WriteLine($"Current temperature: {currentTemp}");
        }

        /// <summary>
        /// Acts on queries from the console.
        /// </summary>
        /// <param name="zoo">The zoo the be used.</param>
        /// <param name="query">The query to be used.</param>
        public static string QueryHelper(Zoo zoo, string query)
        {
            List<Animal> animals;
            string q;

            switch (query)
            {
                case "totalanimalweight":
                    return $"Total animal weight {zoo.Animals.ToList().Sum(a => a.Weight)}";        
                case "averageanimalweight":
                    return $"Average animal weight: {zoo.Animals.ToList().Average(a => a.Weight)}";
                case "animalcount":
                    return $"Total animal count: {zoo.Animals.ToList().Count()}";
                case "firstheavyanimal":
                    zoo.Animals.ToList().ForEach(a => a.Weight);
                    break;
            }

            return "";
        }
    }
}