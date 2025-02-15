using MachineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static Karabelnikov_Var_17.Sort;

namespace Karabelnikov_Var_17
{
    public class ConsoleChoose
    {

        public static void MainMenu(List<Machines> machines)
        {
            while (true)
            {

                string choice = GetUserChoice();

                switch (choice)
                {
                    case "объект":
                        Console.Clear();
                        try
                        {
                            ObjectWork.EditObject(machines);
                        }
                        catch (Exception ex)
                        {
                            ConsoleColor.PrintLine(ex.Message, ConsoleColor.ConsoleColorType.Red);
                        }
                        Console.ReadKey();
                        break;
                    case "сортировка":
                        try
                        {
                            string field = GetSortField();

                            if (field != null)
                            {
                                SortDirection direction = GetSortDirection();
                                SortMachinesByField(machines, field, direction);
                            }
                            Console.ReadKey();
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    case "сохранение":
                        FileWork.SaveToFile(machines);
                        Console.ReadKey();
                        break;
                    case "выход":
                        return;
                    default:
                        ConsoleColor.Print("Некорректный выбор. Повторите попытку.", ConsoleColor.ConsoleColorType.Red);
                        break;
                }
            }
        }


        public static string GetUserChoice()
        {
            Console.Clear();
            // Display menu options.
            ConsoleColor.PrintLine("Меню выбора действия.", ConsoleColor.ConsoleColorType.White);
            ConsoleColor.Print("1. Введите <", ConsoleColor.ConsoleColorType.White);
            ConsoleColor.Print("сортировка", ConsoleColor.ConsoleColorType.Cyan);
            ConsoleColor.PrintLine("> чтобы отсортировать коллекцию объектов по одному из полей.", ConsoleColor.ConsoleColorType.White);

            ConsoleColor.Print("2. Введите <", ConsoleColor.ConsoleColorType.White);
            ConsoleColor.Print("объект", ConsoleColor.ConsoleColorType.Cyan);
            ConsoleColor.PrintLine("> чтобы выбрать объект и отредактировать в нем поле.", ConsoleColor.ConsoleColorType.White);

            ConsoleColor.Print("3. Введите <", ConsoleColor.ConsoleColorType.White);
            ConsoleColor.Print("сохранение", ConsoleColor.ConsoleColorType.Cyan);
            ConsoleColor.PrintLine("> чтобы сохранить коллекцию объектов в файл.", ConsoleColor.ConsoleColorType.White);

            ConsoleColor.Print("4. Введите <", ConsoleColor.ConsoleColorType.White);
            ConsoleColor.Print("выход", ConsoleColor.ConsoleColorType.Cyan);
            ConsoleColor.PrintLine("> чтобы выйти из меню.", ConsoleColor.ConsoleColorType.White);

            Console.WriteLine();
            ConsoleColor.Print("Ваш ввод: ", ConsoleColor.ConsoleColorType.White);

            // Get user choice from the menu.
            return Console.ReadLine().Trim().ToLower();
        }

        



    }
}
