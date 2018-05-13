using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Animals;
using People;
using Reproducers;
using Utilities;

namespace ZooConsole
{
    /// <summary>
    /// The class that provides utility functions for the console application.
    /// </summary>
    internal static class ConsoleUtil
    {
        /// <summary>
        /// Capitalizes the first letter of a string.
        /// </summary>
        /// <param name="value">The string to uppercase.</param>
        /// <returns>The string with the first letter uppercased.</returns>
        public static string InitialUpper(string value)
        {
            return value != null && value.Length > 0 ? char.ToUpper(value[0]) + value.Substring(1) : null;
        }

        /// <summary>
        /// Reads a string value from the console.
        /// </summary>
        /// <param name="prompt">A value to display as an input prompt to the console.</param>
        /// <returns>The string which was read from the console.</returns>
        public static string ReadStringValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "\n] ");

                string stringValue = Console.ReadLine().ToLower().Trim();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads an alphabetic value from the console.
        /// </summary>
        /// <param name="prompt">The prompt to show in the console command.</param>
        /// <returns>The entered string value.</returns>
        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads an integer value from the console.
        /// </summary>
        /// <param name="prompt">The text to display before the prompt.</param>
        /// <returns>A value indicating whether or not the out value is being returned successfully.</returns>
        public static int ReadIntValue(string prompt)
        {
            int result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads an integer value from the console.
        /// </summary>
        /// <param name="prompt">The text to display before the prompt.</param>
        /// <returns>A value indicating whether or not the out value is being returned successfully.</returns>
        public static double ReadDoubleValue(string prompt)
        {
            double result = 0.0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads a gender from console.
        /// </summary>
        /// <returns>The gender that matches the string input.</returns>
        public static Gender ReadGender()
        {
            Gender result = Gender.Female;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<Gender>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads a wallet color from console.
        /// </summary>
        /// <returns>The wallet color that matches the string input.</returns>
        public static WalletColor ReadWalletColor()
        {
            WalletColor result = WalletColor.Black;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Wallet color");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<WalletColor>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid wallet color.");
                }
            }

            return result;
        }

        /// <summary>
        /// Reads an animal type from console.
        /// </summary>
        /// <returns>The animal type that matches the string input.</returns>
        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Chimpanzee;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Animal type");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                // If a matching enumerated value can be found...
                if (Enum.TryParse<AnimalType>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type.");
                }
            }

            return result;
        }

        /// <summary>
        /// Writes details about the help command if it has multiple parameters.
        /// </summary>
        /// <param name="command">The subject of the help details.</param>
        /// <param name="overview">The function of the command.</param>
        /// <param name="arguments">The parameters to the command.</param>
        public static void WriteHelpDetail(string command, string overview, Dictionary<string, string> arguments)
        {
            Console.WriteLine($"Command name: {command}");
            Console.WriteLine($"Overview: {overview}");

            if (arguments != null)
            {
                Console.WriteLine($"Usage: {command} {arguments.Keys.Flatten(" ")}");
                Console.WriteLine("Parameters:");

                arguments.ToList().ForEach(kvp => Console.WriteLine(kvp.Key + ": " + kvp.Value));
            }
        }

        /// <summary>
        /// Writes details about the help command if it only has one parameter.
        /// </summary>
        /// <param name="command">The subject of the help details.</param>
        /// <param name="overview">The function of the command.</param>
        /// <param name="argument">The parameter to the command.</param>
        /// <param name="argumentUsage">The parameter's description and usage.</param>
        public static void WriteHelpDetail(string command, string overview, string argument, string argumentUsage)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(argument, argumentUsage);
            ConsoleUtil.WriteHelpDetail(command, overview, dictionary);
        }

        /// <summary>
        /// Writes details about the help command if it has no parameters.
        /// </summary>
        /// <param name="command">The subject of the help details.</param>
        /// <param name="overview">The function of the command.</param>
        public static void WriteHelpDetail(string command, string overview)
        {
            ConsoleUtil.WriteHelpDetail(command, overview, null);
        }
    }
}