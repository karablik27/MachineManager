using MachineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karabelnikov_Var_17
{
    public static class Sort
    {
        /// <summary>
        /// Enumeration for specifying sort direction.
        /// </summary>
        public enum SortDirection
        {
            /// <summary>
            /// Ascending sort direction.
            /// </summary>
            Ascending = 69,

            /// <summary>
            /// Descending sort direction.
            /// </summary>
            Descending = 6969
        }

        /// <summary>
        /// Sorts a list of Machines objects based on a specified field and direction.
        /// </summary>
        /// <param name="machines">The list of Machines objects to sort.</param>
        /// <param name="field">The field to sort by (brand, model, year, price).</param>
        /// <param name="direction">The sort direction (ascending or descending).</param>
        public static void SortMachinesByField(List<Machines> machines, string field, SortDirection direction)
        {
            // Sort the list of machines based on the specified field and direction
            switch (field)
            {
                case "brand":
                    machines.Sort((m1, m2) => direction == SortDirection.Ascending ?
                        string.Compare(m1.Brand, m2.Brand) :
                        string.Compare(m2.Brand, m1.Brand));
                    break;

                case "model":
                    machines.Sort((m1, m2) => direction == SortDirection.Ascending ?
                        string.Compare(m1.Model, m2.Model) :
                        string.Compare(m2.Model, m1.Model));
                    break;

                case "year":
                    machines.Sort((m1, m2) => direction == SortDirection.Ascending ?
                        m1.Year.CompareTo(m2.Year) :
                        m2.Year.CompareTo(m1.Year));
                    break;

                case "price":
                    machines.Sort((m1, m2) => direction == SortDirection.Ascending ?
                        m1.Price.CompareTo(m2.Price) :
                        m2.Price.CompareTo(m1.Price));
                    break;

                default:
                    ConsoleColor.PrintLine("Некорректное поле для сортировки.", ConsoleColor.ConsoleColorType.Red);
                    break;
            }

            // Display or save the sorted collection
            foreach (var machine in machines)
            {
                // Print a separator line in yellow color to visually separate machines
                ConsoleColor.PrintLine("-------------------------------------------------------------------", ConsoleColor.ConsoleColorType.Yellow);

                // Convert machine object to JSON format and print it
                Console.WriteLine(machine.ToJSON());

                // Print a closing separator line in yellow color for visual separation
                ConsoleColor.PrintLine("-------------------------------------------------------------------", ConsoleColor.ConsoleColorType.Yellow);
            }
        }

        /// <summary>
        /// Prompts the user to select a sort direction and returns the chosen direction.
        /// </summary>
        /// <returns>The selected sort direction.</returns>
        public static SortDirection GetSortDirection()
        {
            // Loop until a valid sort direction is chosen
            while (true)
            {
                ConsoleColor.PrintLine("Выберите направление сортировки:", ConsoleColor.ConsoleColorType.Cyan);
                Console.WriteLine("69. Прямая сортировка");
                Console.WriteLine("6969. Обратная сортировка");
                Console.Write("Ваш выбор: ");

                string input = Console.ReadLine().Trim();

                if (input == "69") // If user chooses ascending direction
                {
                    return SortDirection.Ascending;
                }
                else if (input == "6969") // If user chooses descending direction
                {
                    return SortDirection.Descending;
                }
                else // If user inputs invalid choice
                {
                    ConsoleColor.PrintLine("Некорректный выбор. Повторите попытку.", ConsoleColor.ConsoleColorType.Red);
                }
            }
        }

        /// <summary>
        /// Prompts the user to enter a sort field and returns the chosen field.
        /// </summary>
        /// <returns>The selected sort field.</returns>
        public static string GetSortField()
        {
            // Loop until a valid sort field is entered
            while (true)
            {
                ConsoleColor.Print("Введите поле для сортировки (brand, model, year, price): ", ConsoleColor.ConsoleColorType.Yellow);
                string field = Console.ReadLine().Trim().ToLower();

                // Check the correctness of the entered field
                if (field == "brand" || field == "model" || field == "year" || field == "price")
                {
                    return field; // Return the valid field
                }
                else
                {
                    ConsoleColor.PrintLine("Некорректное поле для сортировки. Повторите попытку.", ConsoleColor.ConsoleColorType.Red);
                }
            }
        }


    }
}
