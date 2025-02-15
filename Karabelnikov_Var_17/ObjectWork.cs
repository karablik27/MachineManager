using MachineLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Karabelnikov_Var_17
{
    /*
           In the event you need to change the status of the repair work of the machines (is_fixed) by default it is false, 
           I have a method that <repairs> breakdowns making their status true, as soon as all breakdowns become with the status true an event is called saying 
           that the machine is ready for work and making the status cars (is_ready) from false to true, it is also possible to add new breakdowns and edit cars.
     */
    public class ObjectWork
    {
        /// <summary>
        /// Method to edit an object in the list of Machines.
        /// </summary>
        /// <param name="machines">List of Machines</param>
        public static void EditObject(List<Machines> machines)
        {
            if (machines == null || !machines.Any()) 
            {
                ConsoleColor.PrintLine("Список машин пуст.", ConsoleColor.ConsoleColorType.Red);
                return;
            }

            int minId = machines.Min(m => m.Machine_ID); 
            int maxId = machines.Max(m => m.Machine_ID); 

            // Отображаем диапазон доступных ID
            Console.WriteLine($"Выберите машину по ID для редактирования (от {minId} до {maxId}):");
            Console.WriteLine();

            while (true) 
            {
                int machineId;
                ConsoleColor.Print("ID машины: ", ConsoleColor.ConsoleColorType.Cyan);
                if (!int.TryParse(Console.ReadLine(), out machineId)) 
                {
                    ConsoleColor.PrintLine("Введите целочисленное значение.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                if (machineId < minId || machineId > maxId) 
                {
                    ConsoleColor.PrintLine($"Машина с таким ID не найдена. Пожалуйста, введите число от {minId} до {maxId}.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                Machines machine = machines.FirstOrDefault(m => m.Machine_ID == machineId);

                if (machine == null) 
                {
                    ConsoleColor.PrintLine("Машина с таким ID не найдена.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }

                // Subscribe to the StatusChanged event for each machine repair job.
                foreach (var repair in machine.Repairs)
                {
                    repair.StatusChanged += (sender, args) => CheckMachineStatus(machine);
                }

                // Variable to control exit from nested loop.
                bool returnToMainMenu = false;

                while (!returnToMainMenu)
                {
                    Console.WriteLine();
                    ConsoleColor.PrintLine("Выберите действие:", ConsoleColor.ConsoleColorType.Cyan);
                    Console.WriteLine("1. Редактировать машину");
                    Console.WriteLine("2. Показать список ремонтных работ");
                    Console.WriteLine("3. Вернуться в главное меню");
                    Console.WriteLine();
                    ConsoleColor.Print("Введите цифру: ", ConsoleColor.ConsoleColorType.Yellow);
                    string actionChoice = Console.ReadLine();

                    switch (actionChoice)
                    {
                        case "1":
                            EditMachine(machine); // Call method to edit the machine
                            break;
                        case "2":
                            ShowRepairs(machine); // Call method to display repairs for the machine
                            bool returnToPrevious = false;

                            while (!returnToPrevious) // Loop until the user returns to the previous menu
                            {
                                Console.WriteLine("Выберите действие:");
                                Console.WriteLine("1. Добавить новую ремонтную работу.");
                                Console.WriteLine("2. Завершить ремонтную работу.");
                                Console.WriteLine("3. Вернуться в предыдущее меню.");
                                Console.WriteLine("4. Вернуться к выбору ID машины.");
                                Console.WriteLine();
                                ConsoleColor.Print("Введите цифру: ", ConsoleColor.ConsoleColorType.Yellow);
                                string subActionChoice = Console.ReadLine();

                                switch (subActionChoice)
                                {
                                    case "1":
                                        AddNewRepair(machine); // Call method to add a new repair
                                        break;
                                    case "2":
                                        MarkRepairAsCompleted(machine); // Call method to mark a repair as completed
                                        break;
                                    case "3":
                                        // Return to selecting an action for the machine without leaving the method.
                                        returnToPrevious = true;
                                        break;
                                    case "4":
                                        // Return to the main menu, exit the method.
                                        returnToMainMenu = true;
                                        break;
                                    default:
                                        ConsoleColor.PrintLine("Некорректный выбор действия.", ConsoleColor.ConsoleColorType.Red);
                                        break;
                                }
                                // Exit nested loop.
                                if (returnToMainMenu) break;
                            }
                            // Exit main loop.
                            if (returnToMainMenu) break;
                            continue; // Continue working with selecting actions for the machine.
                        case "3":
                            ConsoleColor.PrintLine("Нажмите что-нибудь для выхода в главное меню.", ConsoleColor.ConsoleColorType.Green);
                            return; // Return to the main menu, exit the method.
                        default:
                            ConsoleColor.PrintLine("Некорректный выбор действия.", ConsoleColor.ConsoleColorType.Red);
                            break;
                    }
                }
            }
        }

        





        /// <summary>
        /// Method to mark a repair as completed for a given machine.
        /// </summary>
        /// <param name="machine">Machine object</param>
        public static void MarkRepairAsCompleted(Machines machine)
        {
            Console.WriteLine($"Изменение статуса ремонтной работы для машины ID {machine.Machine_ID}:");
            ShowRepairs(machine);

            string repairId = string.Empty; 
            bool isValidRepairId = false;

            while (!isValidRepairId)
            {
                Console.Write("Введите ID ремонтной работы для изменения статуса(например если выбрана машина с ID = 1 , и вы хотите изменить ее первую работу введите R1-1): ");
                repairId = Console.ReadLine();

                // Checking the correctness of the repair work ID.
                if (!IsValidRepairIdForDelete(repairId, machine))
                {
                    ConsoleColor.PrintLine("Ремонтная работа с указанным ID не найдена. Повторите ввод.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }
                // If the ID is correct, exit the loop.
                isValidRepairId = true; 
            }

            // Find a repair job based on the entered ID.
            var repairToComplete = machine.Repairs.FirstOrDefault(r => r.Repair_ID == repairId);

            // Mark the repair job as completed (is_fixed = true)
            if (repairToComplete != null)
            {
                repairToComplete.IS_Fixed = true;
                ConsoleColor.PrintLine("Статус ремонтной работы успешно изменен.", ConsoleColor.ConsoleColorType.Green);
            }
            else
            {
                /* 
                   This block of code should not be executed because the ID check has already been done,
                   but it is left in place to ensure code reliability.
                */
                ConsoleColor.PrintLine("Ремонтная работа с указанным ID не найдена.", ConsoleColor.ConsoleColorType.Red);
            }

            
        }



        /// <summary>
        /// Method to edit the details of a machine.
        /// </summary>
        /// <param name="machine">Machine object</param>
        public static void EditMachine(Machines machine)
        {
            // Display current brand value in cyan color
            ConsoleColor.PrintLine($"Текущее значение бренда: {machine.Brand}", ConsoleColor.ConsoleColorType.Cyan);

            // Prompt user for new brand value and update machine object
            Console.Write("Введите новое значение бренда: ");
            string newBrand = Console.ReadLine();
            machine.Brand = newBrand;

            // Display current model value in cyan color
            ConsoleColor.PrintLine($"Текущее значение модели: {machine.Model}", ConsoleColor.ConsoleColorType.Cyan);

            // Prompt user for new model value and update machine object
            Console.Write("Введите новое значение модели: ");
            string newModel = Console.ReadLine();
            machine.Model = newModel;

            // Display current year value in cyan color
            ConsoleColor.PrintLine($"Текущее значение года выпуска: {machine.Year}", ConsoleColor.ConsoleColorType.Cyan);

            int newYear;
            bool isValidYear = false;

            do
            {
                // Prompt user for new year value and validate it
                Console.Write("Введите новое значение года выпуска: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out newYear))
                {
                    // Display error message if input format is incorrect
                    ConsoleColor.PrintLine("Некорректный формат ввода. Попробуйте еще раз.", ConsoleColor.ConsoleColorType.Red);
                }
                else if (newYear <= 0)
                {
                    // Display error message if input year is not positive
                    ConsoleColor.PrintLine("Год выпуска должен быть положительным числом. Попробуйте еще раз.", ConsoleColor.ConsoleColorType.Red);
                }
                else
                {
                    isValidYear = true; // Set flag to exit loop if year is valid
                }
            } while (!isValidYear);

            machine.Year = newYear; // Update machine object with new valid year

            // Display current price value in cyan color
            ConsoleColor.PrintLine($"Текущее значение цены: {machine.Price}", ConsoleColor.ConsoleColorType.Cyan);

            // Prompt user for new price value and update machine object
            Console.Write("Введите новое значение цены: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());
            machine.Price = newPrice;

            // Display success message after machine is successfully edited
            ConsoleColor.PrintLine("Машина успешно отредактирована.", ConsoleColor.ConsoleColorType.Green);
        }


        /// <summary>
        /// Method to add a new repair for a given machine.
        /// </summary>
        /// <param name="machine">Machine object</param>
        public static void AddNewRepair(Machines machine)
        {
            // Display message indicating the addition of a new repair for the machine
            Console.WriteLine($"Добавление новой ремонтной работы для машины ID {machine.Machine_ID}:");
            string repairId;

            while (true)
            {
                Console.Write("ID ремонтной работы: ");
                repairId = Console.ReadLine();

                // Validate the format and uniqueness of the repair ID
                if (!IsValidRepairId(repairId, machine))
                {
                    ConsoleColor.PrintLine("Повторите ввод.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }
                break; // Exit the loop if the repair ID is correct and unique
            }

            // Prompt user for description of the issue
            Console.Write("Описание проблемы: ");
            string issue = Console.ReadLine();

            decimal repairCost;
            while (true)
            {
                // Prompt user for repair cost and validate it
                Console.Write("Стоимость ремонта: ");
                if (!decimal.TryParse(Console.ReadLine(), out repairCost) || repairCost <= 0)
                {
                    ConsoleColor.PrintLine("Введите положительное число для стоимости ремонта.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }
                break;
            }

            // Prompt user for technician name
            Console.Write("Техник: ");
            string technician = Console.ReadLine();

            bool isFixed;
            while (true)
            {
                // Prompt user for repair completion status and validate it
                Console.Write("Ремонт завершен (true/false): ");
                if (!bool.TryParse(Console.ReadLine(), out isFixed))
                {
                    ConsoleColor.PrintLine("Введите 'true' или 'false'.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }
                break;
            }

            // Prompt user for repair date in format yyyy-MM-dd
            Console.Write("Дата ремонта (гггг-мм-дд): ");
            DateTime repairDate;
            while (true)
            {
                if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out repairDate))
                {
                    ConsoleColor.PrintLine("Неверный формат даты. Введите дату в формате 'гггг-мм-дд'.", ConsoleColor.ConsoleColorType.Red);
                    continue;
                }
                break;
            }

            // Create a new instance of Repairs class with user-provided information
            Repairs newRepair = new Repairs(repairId, issue, repairCost, technician, isFixed, repairDate);

            // Add the new repair to the machine's list of repairs
            machine.AddRepair(newRepair);

            // Display success message after adding the new repair
            ConsoleColor.PrintLine("Новая ремонтная работа успешно добавлена.", ConsoleColor.ConsoleColorType.Green);
        }





        /// <summary>
        /// Method to check the validity of a repair ID for addition.
        /// </summary>
        /// <param name="repairId">ID of the repair</param>
        /// <param name="machine">Machine object</param>
        /// <returns>True if the repair ID is valid, otherwise false</returns>
        private static bool IsValidRepairId(string repairId, Machines machine)
        {
            // Check the format of the repair ID using a regular expression
            if (!Regex.IsMatch(repairId, @"^R\d+-\d+$"))
            {
                // Display an error message if the format is incorrect
                ConsoleColor.PrintLine("Неверный формат ID.", ConsoleColor.ConsoleColorType.Red);
                return false;
            }

            // Extract the machine number from the repair ID
            int machineId;
            if (!int.TryParse(repairId.Split('-')[0].Substring(1), out machineId))
            {
                // Display an error message if the machine number in the ID is incorrect
                ConsoleColor.PrintLine("Некорректный номер машины в ID ремонтной работы.", ConsoleColor.ConsoleColorType.Red);
                return false;
            }

            // Check if the machine number in the ID matches the ID of the selected machine
            if (machineId != machine.Machine_ID)
            {
                // Display an error message if the repair ID doesn't correspond to the selected machine
                ConsoleColor.PrintLine("ID ремонтной работы не соответствует выбранной машине.", ConsoleColor.ConsoleColorType.Red);
                return false;
            }

            // Check if there is already a repair with the specified ID for this machine
            if (machine.Repairs.Any(repair => repair.Repair_ID == repairId))
            {
                // Display an error message if a repair with the specified ID already exists
                ConsoleColor.PrintLine("Ремонтная работа с указанным ID уже существует.", ConsoleColor.ConsoleColorType.Red);
                return false;
            }

            return true; // Return true if all checks pass
        }


        /// <summary>
        /// Method to check the validity of a repair ID for deletion.
        /// </summary>
        /// <param name="repairId">ID of the repair</param>
        /// <param name="machine">Machine object</param>
        /// <returns>True if the repair ID is valid for deletion, otherwise false</returns>
        private static bool IsValidRepairIdForDelete(string repairId, Machines machine)
        {
            // Check the format of the repair ID using a regular expression
            if (!Regex.IsMatch(repairId, @"^R\d+-\d+$"))
            {
                // Display an error message if the format is incorrect
                ConsoleColor.PrintLine("Неверный формат ID.", ConsoleColor.ConsoleColorType.Red);
                return false;
            }

            // Check if there is a repair with the specified ID for this machine
            return machine.Repairs.Any(repair => repair.Repair_ID == repairId);
        }









        /*
           Initially there was a method for deleting repair work because 
           I didn’t understand the condition of the problem and thought that
           I needed to add or remove repair work, but it turned out that I needed to change their status is_fixed to true or false
         */

        /// <summary>
        /// Method to remove a repair from a machine.
        /// </summary>
        /// <param name="machine">Machine object</param>
        public static void RemoveRepair(Machines machine)
        {
            // Display message indicating the change of repair status for the machine
            Console.WriteLine($"Изменение статуса ремонтной работы для машины ID {machine.Machine_ID}:");

            // Show the list of repairs for the machine
            ShowRepairs(machine);

            // Prompt the user to input the ID of the repair to change its status
            Console.Write("Введите ID ремонтной работы для изменения статуса: ");
            string repairIdToChange = Console.ReadLine();

            // Find the repair with the specified ID for the machine
            Repairs repairToChange = machine.Repairs.FirstOrDefault(r => r.Repair_ID == repairIdToChange);

            if (repairToChange != null)
            {
                // Check if the repair is not already marked as fixed
                if (!repairToChange.IS_Fixed)
                {
                    repairToChange.IS_Fixed = true; // Change the status of the repair to fixed
                    ConsoleColor.PrintLine("Статус ремонтной работы успешно изменен.", ConsoleColor.ConsoleColorType.Green);
                }
                else
                {
                    ConsoleColor.PrintLine("Ремонтная работа уже завершена.", ConsoleColor.ConsoleColorType.Yellow);
                }
            }
            else
            {
                ConsoleColor.PrintLine("Ремонтная работа с указанным ID не найдена.", ConsoleColor.ConsoleColorType.Red);
            }

            // Check if all repairs for the machine are fixed
            if (machine.Repairs.All(r => r.IS_Fixed))
            {
                machine.IS_Ready = true; // Mark the machine as ready for use
                ConsoleColor.PrintLine("Машина помечена как готовая к использованию.", ConsoleColor.ConsoleColorType.Yellow);
            }
        }


        /// <summary>
        /// Method to display the list of repairs for a given machine.
        /// </summary>
        /// <param name="machine">Machine object</param>
        public static void ShowRepairs(Machines machine)
        {
            // Display message indicating the list of repairs for the machine
            Console.WriteLine($"Список ремонтных работ для машины ID {machine.Machine_ID}:");

            // Iterate through each repair for the machine and display its details
            foreach (var repair in machine.Repairs)
            {
                // Print a separator line in yellow color to visually separate repairs
                ConsoleColor.PrintLine("--------------------------------------------------------------", ConsoleColor.ConsoleColorType.Yellow);

                // Print the ID of the repair
                Console.WriteLine($"ID ремонтной работы: {repair.Repair_ID}");

                // Print the description of the issue
                Console.WriteLine($"Описание проблемы: {repair.Issue}");

                // Print the repair cost
                Console.WriteLine($"Стоимость ремонта: {repair.Repair_Cost}");

                // Print the technician responsible for the repair
                Console.WriteLine($"Техник: {repair.Technician}");

                // Print whether the repair is fixed or not
                Console.WriteLine($"Ремонт завершен: {(repair.IS_Fixed ? "Да" : "Нет")}");

                // Print the date of the repair in the format dd.MM.yyyy
                Console.WriteLine($"Дата ремонта: {repair.Repair_Date.ToString("dd.MM.yyyy")}");
            }

            // Print a closing separator line in yellow color for visual separation
            ConsoleColor.PrintLine("--------------------------------------------------------------", ConsoleColor.ConsoleColorType.Yellow);

            // Print an empty line for better readability
            Console.WriteLine();
        }


        /// <summary>
        /// Method to check and update the status of a machine.
        /// </summary>
        /// <param name="machine">Machine object</param>
        private static void CheckMachineStatus(Machines machine)
        {
            // Check if all repairs for the machine are marked as fixed
            if (machine.Repairs.All(r => r.IS_Fixed))
            {
                // If all repairs are fixed, mark the machine as ready for use
                machine.IS_Ready = true;
                ConsoleColor.PrintLine("Машина помечена как готовая к использованию.", ConsoleColor.ConsoleColorType.Yellow);
            }
        }



    }
}
