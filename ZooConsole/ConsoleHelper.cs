using System;
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
                Console.WriteLine($"Previous temperature: {previousTemp:0.0}.");
                Console.WriteLine($"New temperature: {zoo.BirthingRoomTemperature:0.0}.");
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
            Animal animal = zoo.FindAnimal(name);

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
        /// Shows a guest in the console.
        /// </summary>
        /// <param name="zoo">The zoo in which the guest resides.</param>
        /// <param name="name">The name of the guest to show.</param>
        private static void ShowGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(name);

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
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="zoo">The current zoo.</param>
        /// <param name="name">The name of the animal to remove.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animalToRemove = zoo.FindAnimal(name);

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
            Guest guestToRemove = zoo.FindGuest(name);

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
        /// Shows the cage in the console.
        /// </summary>
        /// <param name="zoo">The zoo whose cage is to be shown.</param>
        /// <param name="animalName">The animal in the cage.</param>
        private static void ShowCage(Zoo zoo, string animalName)
        {
            Animal animal = zoo.FindAnimal(animalName);
            Cage cage = zoo.FindCage(animal.GetType());
            Console.WriteLine(cage.ToString());
        }
    }
}