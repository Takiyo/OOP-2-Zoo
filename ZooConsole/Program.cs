using System;
using Animals;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// Contains interaction logic for the console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private static Zoo zoo;

        /// <summary>
        /// The program's main (start-up) method.
        /// </summary>
        /// <param name="args">The method arguments for the console application.</param>
        public static void Main(string[] args)
        {
            // Set window title.
            Console.Title = "Object-Oriented Programming 2: Zoo";

            // Write introductory message.
            Console.WriteLine("Welcome to the Como Zoo!");

            // Create zoo instance.
            Zoo zoo = Zoo.NewZoo();

            bool exit = false;

            string command;

            try
            {
                while (!exit)
                {
                    Console.Write("] ");
                    command = Console.ReadLine();
                    string[] commandWords = command.ToLower().Trim().Split();

                    switch (commandWords[0])
                    {
                        case "exit":
                            exit = true;
                            break;
                        case "restart":
                            zoo = Zoo.NewZoo();
                            Console.WriteLine("A new Como Zoo has been created.");
                            break;
                        case "help":
                            Console.WriteLine("Known commands:");
                            Console.WriteLine("HELP: Shows a list of known commands.");                            
                            Console.WriteLine("EXIT: Exits the application.");
                            Console.WriteLine("RESTART: Creates a new zoo.");
                            Console.WriteLine("TEMPERATURE: Sets the birthing room temperature.");
                            Console.WriteLine("SHOW ANIMAL [animal name]: Displays information for specified animal.");
                            Console.WriteLine("SHOW GUEST [guest name]: Displays information for specified guest.");
                            Console.WriteLine("ADD: Adds an animal or guest to the zoo.");
                            Console.WriteLine("REMOVE: Removes an animal or guest from the zoo.");
                            
                            break;
                        case "temp":
                            try
                            {
                                ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter a parameter for temperature.");
                            }

                            break;
                        case "show":
                            try
                            {
                                ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal, guest, or cage] [name].");
                            }

                            break;
                        case "remove":
                            try
                            {
                                ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest] [name].");
                            }

                            break;
                        case "add":
                            try
                            {
                                ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest].");
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Zoo is out of tickets.");
                            }
                            break;
                        case "sort":
                            try
                            {
                                SortResult result = zoo.SortAnimals(commandWords[1], commandWords[2]);

                                Console.WriteLine($"SORT TYPE: {commandWords[1].ToUpper()}");
                                Console.WriteLine($"SORT BY: {commandWords[2].ToUpper()}");
                                Console.WriteLine($"SWAP COUNT: {result.SwapCount}");

                                foreach (Animal a in result.Animals)
                                {
                                    Console.WriteLine(a.ToString());
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort type] [sort by -- weight or name].");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid command entered.");

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}