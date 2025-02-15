using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karabelnikov_Var_17
{
    public class ConsoleColor
    {
        /// <summary>
        /// Enum defining console text colors.
        /// </summary>
        public enum ConsoleColorType
        {
            Black,
            DarkBlue,
            DarkGreen,
            DarkCyan,
            DarkRed,
            DarkMagenta,
            DarkYellow,
            Gray,
            DarkGray,
            Blue,
            Green,
            Cyan,
            Red,
            Magenta,
            Yellow,
            White
        }

        /// <summary>
        /// Prints the specified message to the console with the specified text color.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <param name="colorType">The text color for the console output.</param>
        public static void Print(string message, ConsoleColorType colorType)
        {
            System.ConsoleColor color;

            // Convert ConsoleColorType to System.ConsoleColor
            switch (colorType)
            {
                case ConsoleColorType.Black:
                    color = System.ConsoleColor.Black;
                    break;
                case ConsoleColorType.DarkBlue:
                    color = System.ConsoleColor.DarkBlue;
                    break;
                case ConsoleColorType.DarkGreen:
                    color = System.ConsoleColor.DarkGreen;
                    break;
                case ConsoleColorType.DarkCyan:
                    color = System.ConsoleColor.DarkCyan;
                    break;
                case ConsoleColorType.DarkRed:
                    color = System.ConsoleColor.DarkRed;
                    break;
                case ConsoleColorType.DarkMagenta:
                    color = System.ConsoleColor.DarkMagenta;
                    break;
                case ConsoleColorType.DarkYellow:
                    color = System.ConsoleColor.DarkYellow;
                    break;
                case ConsoleColorType.Gray:
                    color = System.ConsoleColor.Gray;
                    break;
                case ConsoleColorType.DarkGray:
                    color = System.ConsoleColor.DarkGray;
                    break;
                case ConsoleColorType.Blue:
                    color = System.ConsoleColor.Blue;
                    break;
                case ConsoleColorType.Green:
                    color = System.ConsoleColor.Green;
                    break;
                case ConsoleColorType.Cyan:
                    color = System.ConsoleColor.Cyan;
                    break;
                case ConsoleColorType.Red:
                    color = System.ConsoleColor.Red;
                    break;
                case ConsoleColorType.Magenta:
                    color = System.ConsoleColor.Magenta;
                    break;
                case ConsoleColorType.Yellow:
                    color = System.ConsoleColor.Yellow;
                    break;
                case ConsoleColorType.White:
                    color = System.ConsoleColor.White;
                    break;
                default:
                    color = System.ConsoleColor.White; // Default to white color
                    break;
            }

            Console.ForegroundColor = color; // Set text color
            Console.Write(message); // Write message to console
            Console.ResetColor(); // Reset console color
        }

        /// <summary>
        /// Prints the specified message followed by a new line to the console with the specified text color.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <param name="colorType">The text color for the console output.</param>
        public static void PrintLine(string message, ConsoleColorType colorType)
        {
            System.ConsoleColor color;

            // Convert ConsoleColorType to System.ConsoleColor
            switch (colorType)
            {
                case ConsoleColorType.Black:
                    color = System.ConsoleColor.Black;
                    break;
                case ConsoleColorType.DarkBlue:
                    color = System.ConsoleColor.DarkBlue;
                    break;
                case ConsoleColorType.DarkGreen:
                    color = System.ConsoleColor.DarkGreen;
                    break;
                case ConsoleColorType.DarkCyan:
                    color = System.ConsoleColor.DarkCyan;
                    break;
                case ConsoleColorType.DarkRed:
                    color = System.ConsoleColor.DarkRed;
                    break;
                case ConsoleColorType.DarkMagenta:
                    color = System.ConsoleColor.DarkMagenta;
                    break;
                case ConsoleColorType.DarkYellow:
                    color = System.ConsoleColor.DarkYellow;
                    break;
                case ConsoleColorType.Gray:
                    color = System.ConsoleColor.Gray;
                    break;
                case ConsoleColorType.DarkGray:
                    color = System.ConsoleColor.DarkGray;
                    break;
                case ConsoleColorType.Blue:
                    color = System.ConsoleColor.Blue;
                    break;
                case ConsoleColorType.Green:
                    color = System.ConsoleColor.Green;
                    break;
                case ConsoleColorType.Cyan:
                    color = System.ConsoleColor.Cyan;
                    break;
                case ConsoleColorType.Red:
                    color = System.ConsoleColor.Red;
                    break;
                case ConsoleColorType.Magenta:
                    color = System.ConsoleColor.Magenta;
                    break;
                case ConsoleColorType.Yellow:
                    color = System.ConsoleColor.Yellow;
                    break;
                case ConsoleColorType.White:
                    color = System.ConsoleColor.White;
                    break;
                default:
                    color = System.ConsoleColor.White; // Default to white color
                    break;
            }

            Console.ForegroundColor = color; // Set text color
            Console.WriteLine(message); // Write message to console followed by new line
            Console.ResetColor(); // Reset console color
        }

    }
}
