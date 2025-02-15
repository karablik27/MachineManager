using MachineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karabelnikov_Var_17
{
    public class FileWork
    {
        /// <summary>
        /// Reads the absolute path to a JSON data file from the user input.
        /// </summary>
        /// <returns>The absolute path to the JSON data file.</returns>
        public static string Read()
        {
            string? filename;

            // Keep prompting the user until a valid file path is provided.
            while (true)
            {
                // Prompt the user to enter the absolute path to the JSON file.
                ConsoleColor.Print("Введите абсолютный путь к файлу с json-данными:  ", ConsoleColor.ConsoleColorType.White);
                filename = Console.ReadLine();

                // Check if the specified file exists.
                if (!File.Exists(filename))
                {
                    // Print an error message if the specified file does not exist and continue to prompt for input.
                    ConsoleColor.PrintLine("Указанный файл не существует, повторите ввод.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                // Check if the file has a .json extension.
                if (!filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    // Print an error message if the specified file is not a JSON file and continue to prompt for input.
                    ConsoleColor.PrintLine("Указанный файл не является файлом json, повторите ввод.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                // If a valid file path is provided, exit the loop.
                break;
            }

            return filename;
        }


        /// <summary>
        /// Prompts the user to provide a file name or full file path for saving or overwriting.
        /// </summary>
        /// <returns>The file path for saving or overwriting.</returns>
        public static string GetFilePathToWrite()
        {
            string? file;

            // Keep prompting until a valid file path is provided.
            while (true)
            {
                ConsoleColor.Print("Введите названия файла или полный путь файла для сохранения или его перезаписи:", ConsoleColor.ConsoleColorType.Yellow);
                file = Console.ReadLine();

                // Check if the file path is empty.
                if (string.IsNullOrEmpty(file))
                {
                    ConsoleColor.PrintLine("Не введено имя файла, пожалуйста, попробуйте еще раз.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                // Check if the file has a .json extension.
                if (!file.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    ConsoleColor.PrintLine("Указанный файл не является файлом json, пожалуйста, попробуйте еще раз.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                // Check if the directory exists (if a path is provided).
                string directoryPath = Path.GetDirectoryName(file);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    ConsoleColor.PrintLine("Директория не существует, пожалуйста, введите допустимый путь.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                // Check if the file exists and confirm overwrite.
                if (File.Exists(file))
                {
                    ConsoleColor.PrintLine("Файл уже существует. Хотите его перезаписать? (да/нет)", ConsoleColor.ConsoleColorType.Yellow);
                    string? overwriteConfirmation = Console.ReadLine();
                    if (!overwriteConfirmation.Equals("Да", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; // If overwrite is not confirmed, prompt for the path again.
                    }
                }

                break; // Valid path provided.
            }

            return file;
        }

        /// <summary>
        /// Saves the list of machines to a JSON file after getting the file path from the user.
        /// </summary>
        /// <param name="machines">The list of machines to save.</param>
        public static void SaveToFile(List<Machines> machines)
        {
            // Loop until data is successfully saved to a file or an exception occurs.
            while (true)
            {
                // Checking for exception
                try
                {
                    // Get the file path to write.
                    string saveFilePath = GetFilePathToWrite();

                    // Save the list of machines to a JSON file.
                    JsonWriter.SaveToJsonFile(saveFilePath, machines);

                    // Print a success message indicating that data has been successfully written to the file.
                    ConsoleColor.PrintLine("Данные успешно записаны в файл!", ConsoleColor.ConsoleColorType.Green);

                    // Exit the loop as data is successfully saved.
                    break;
                }
                catch (Exception ex)
                {
                    // Print the exception message if an error occurs during the save operation.
                    ConsoleColor.PrintLine(ex.Message, ConsoleColor.ConsoleColorType.Red);

                    // Continue to the next iteration of the loop to retry saving the data.
                    continue;
                }
            }
        }



    }
}
