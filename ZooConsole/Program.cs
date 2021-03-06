﻿using System;
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
            ConsoleHelper.AttachDelegates(zoo);

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
                            ConsoleHelper.AttachDelegates(zoo);

                            Console.WriteLine("A new Como Zoo has been created.");
                            break;
                        case "help":
                            if (commandWords.Length == 1)
                            {
                                ConsoleHelper.ShowHelp();
                            }
                            else if (commandWords.Length == 2)
                            {
                                ConsoleHelper.ShowHelpDetail(commandWords[1]);
                            }
                            else
                            {
                                Console.WriteLine("Too many parameters entered for the HELP command.");
                            }

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
                                SortResult result = new SortResult();

                                if (commandWords[1] == "animals")
                                {
                                    if (commandWords[2] == "binary")
                                    {
                                        int loopCounter = 0;
                                        string name = commandWords[3];
                                        SortResult animals = zoo.SortAnimals("bubble", "name");
                                        int minPosition = 0;
                                        int maxPosition = animals.Objects.Count - 1;
                                        int middlePosition = 0;
                                        int compareResult;
                                        while (minPosition <= maxPosition)
                                        {
                                            middlePosition = (minPosition + maxPosition) / 2;

                                            loopCounter++;

                                            compareResult = string.Compare(name, (animals.Objects[middlePosition] as Animal).Name.ToLower());

                                            if (compareResult > 0)
                                            {
                                                minPosition = middlePosition + 1;
                                            }
                                            else if (compareResult < 0)
                                            {
                                                maxPosition = middlePosition - 1;
                                            }
                                            else
                                            {
                                                Console.WriteLine($"{name} found. {loopCounter} loops complete.");
                                                break;
                                            }
                                        }
                                    }
                                    result = zoo.SortAnimals(commandWords[2], commandWords[3]);
                                    Console.WriteLine($"SORT TYPE: {commandWords[2].ToUpper()}");
                                    Console.WriteLine($"SORT BY: {commandWords[1].ToUpper()}");
                                    Console.WriteLine($"SORT VALUE: {commandWords[3]}");
                                    Console.WriteLine($"SWAP COUNT: {result.SwapCount}");
                                    Console.WriteLine($"COMPARE COUNT: {result.CompareCount}");
                                    Console.WriteLine($"TIME: {result.ElapsedMilliseconds}");
                                    foreach (Object o in result.Objects)
                                    {
                                        Console.WriteLine(o.ToString());
                                    }
                                }

                                if (commandWords[1] == "guests")
                                {
                                    if (commandWords[2] == "binary")
                                    {
                                        int loopCounter = 0;
                                        string name = commandWords[3];
                                        SortResult guests = zoo.SortGuests("bubble", "name");
                                        int minPosition = 0;
                                        int maxPosition = guests.Objects.Count - 1;
                                        int middlePosition = 0;
                                        int compareResult;
                                        while (minPosition <= maxPosition)
                                        {
                                            middlePosition = (minPosition + maxPosition) / 2;

                                            loopCounter++;

                                            compareResult = string.Compare(name, (guests.Objects[middlePosition] as Guest).Name.ToLower());

                                            if (compareResult > 0)
                                            {
                                                minPosition = middlePosition + 1;
                                            }
                                            else if (compareResult < 0)
                                            {
                                                maxPosition = middlePosition - 1;
                                            }
                                            else
                                            {
                                                Console.WriteLine($"{name} found. {loopCounter} loops complete.");
                                                break;
                                            }
                                        }
                                    }
                                    result = zoo.SortGuests(commandWords[2], commandWords[3]);
                                    Console.WriteLine($"SORT TYPE: {commandWords[2].ToUpper()}");
                                    Console.WriteLine($"SORT BY: {commandWords[1].ToUpper()}");
                                    Console.WriteLine($"SORT VALUE: {commandWords[3]}");
                                    Console.WriteLine($"SWAP COUNT: {result.SwapCount}");
                                    Console.WriteLine($"COMPARE COUNT: {result.CompareCount}");
                                    Console.WriteLine($"TIME: {result.ElapsedMilliseconds}");
                                    foreach (Object o in result.Objects)
                                    {
                                        Console.WriteLine(o.ToString());
                                    }
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort type] [sort by -- weight or name].");
                            }
                            break;
                        case "search":
                            if (commandWords[1] == "linear")
                            {
                                int loopCounter = 0;
                                string name = commandWords[2];
                                foreach (Animal a in zoo.Animals)
                                {
                                    loopCounter++;
                                    if (a.Name == name)
                                    {
                                        Console.WriteLine($"{name} found. {loopCounter} loops complete.");
                                    }
                                }
                            }                      
                            break;

                        case "query":
                            Console.WriteLine(ConsoleHelper.QueryHelper(zoo, commandWords[1]));

                            break;
                        case "save":
                            ConsoleHelper.SaveFile(zoo, commandWords[1]);
                            break;
                        case "load":
                            ConsoleHelper.LoadFile(commandWords[1]);
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