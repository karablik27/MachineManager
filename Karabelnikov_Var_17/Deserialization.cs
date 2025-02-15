using MachineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Karabelnikov_Var_17
{
    public class Deserialization
    {
        /// <summary>
        /// Deserialize JSON data from the specified file into a list of Machines objects and subscribe them to auto-saving.
        /// </summary>
        /// <param name="filename">The path to the JSON file to deserialize.</param>
        /// <returns>A list of Machines objects deserialized from the JSON file.</returns>
        public static List<Machines> DeserializeJson(string filename)
        {
            List<Machines>? machines;

            // Checking for exceptions
            try
            {
                // Read JSON data from file
                string jsonString = File.ReadAllText(filename);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Deserialize JSON data into list of Machines objects
                machines = JsonSerializer.Deserialize<List<Machines>>(jsonString, options);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"ArgumentException: {ex.Message}");
            }
            catch (IOException ex)
            {
                throw new IOException($"IOException: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new JsonException($"JsonException: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }

            if (machines is null || machines.Count == 0)
            {
                return null;
            }

            try
            {
                // Subscribe machines to auto-saving
                machines = SubscribeToAutoSaver(machines, filename);
            }
            catch (Exception ex)
            {
                ConsoleColor.PrintLine(ex.Message, ConsoleColor.ConsoleColorType.Red);
            }

            return machines;
        }

        /// <summary>
        /// Subscribes machines and their repairs to an AutoSaver instance for automatic saving.
        /// </summary>
        /// <param name="machines">The list of machines to subscribe.</param>
        /// <param name="file">The file associated with the AutoSaver.</param>
        /// <returns>The list of machines after subscribing to AutoSaver.</returns>
        private static List<Machines> SubscribeToAutoSaver(List<Machines> machines, string file)
        {
            // Dictionary to hold repairs associated with each machine
            Dictionary<string, Repairs> repairsDictionary = new Dictionary<string, Repairs>();

            AutoSaver autoSaver;

            // Checking for exceptions
            try
            {
                // Initialize AutoSaver with the file and list of machines
                autoSaver = new AutoSaver(file, machines);
            }
            catch (Exception ex)
            {
                // Handle error initializing AutoSaver
                ConsoleColor.PrintLine($"Error initializing AutoSaver: {ex.Message}", ConsoleColor.ConsoleColorType.Red);
                return machines;
            }

            // Subscribe each machine and its repairs to the AutoSaver
            if (machines != null)
            {
                foreach (Machines machine in machines)
                {
                    try
                    {
                        // Subscribe machine to AutoSaver
                        autoSaver.SubscribeToMachine(machine);

                        // Subscribe repairs of the machine to AutoSaver
                        if (machine.Repairs != null)
                        {
                            foreach (Repairs repair in machine.Repairs)
                            {
                                try
                                {
                                    autoSaver.SubscribeToRepair(repair);
                                }
                                catch (Exception ex)
                                {
                                    // Handle error subscribing to repairs
                                    ConsoleColor.PrintLine($"Error subscribing to repairs: {ex.Message}", ConsoleColor.ConsoleColorType.Red);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle error subscribing to machines
                        ConsoleColor.PrintLine($"Error subscribing to machines: {ex.Message}", ConsoleColor.ConsoleColorType.Red);
                    }
                }
            }

            return machines;
        }


    }

}
