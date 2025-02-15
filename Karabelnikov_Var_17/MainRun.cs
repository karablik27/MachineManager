using MachineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karabelnikov_Var_17
{
    public class MainRun
    {
        /// <summary>
        /// Loads machines from a JSON file and displays them on the console.
        /// </summary>
        public static void LoadAndDisplayMachinesFromJson()
        {
            do
            {
                Console.Clear();

                // Read the path to the JSON file.
                string? jsonfile = FileWork.Read();

                List<Machines>? machines = null;

                try
                {
                    // Deserialize JSON data into a list of Machines objects.
                    machines = Deserialization.DeserializeJson(jsonfile);

                    if (machines is not null && machines.Count != 0)
                    {
                        // Display the loaded data from the JSON file.
                        ConsoleColor.PrintLine("Считанные данные из Json файла: ", ConsoleColor.ConsoleColorType.Green);

                        foreach (var machine in machines)
                        {
                            ConsoleColor.PrintLine("--------------------------------------------------------------", ConsoleColor.ConsoleColorType.Yellow);
                            Console.WriteLine(machine.ToJSON());
                            ConsoleColor.PrintLine("--------------------------------------------------------------", ConsoleColor.ConsoleColorType.Yellow);
                        }
                    }
                    else
                    {
                        ConsoleColor.PrintLine("Считанный Json файл пуст", ConsoleColor.ConsoleColorType.Red);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleColor.PrintLine(ex.Message, ConsoleColor.ConsoleColorType.Red);
                }

                Console.WriteLine();
                ConsoleColor.PrintLine("Нажмите что-нибдуь, чтобы продолжить.", ConsoleColor.ConsoleColorType.Yellow);
                Console.ReadKey();

                try
                {
                    // Navigate to the main menu.
                    ConsoleChoose.MainMenu(machines);
                }
                catch (Exception e)
                {
                    ConsoleColor.PrintLine(e.Message, ConsoleColor.ConsoleColorType.Red);
                }

                ConsoleColor.PrintLine("Для завершения программы нажмите Escape, для продолжения любую другую клавишу.", ConsoleColor.ConsoleColorType.Cyan);
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

    }
}
