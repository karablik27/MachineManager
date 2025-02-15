using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLibrary
{
    public class AutoSaver
    {
        private List<Machines> _machines; // List to store machines data
        private string _originalFilePath; // Path of the original file
        private DateTime _lastEventTime = DateTime.MinValue; // Timestamp of the last event handled
        private TimeSpan _maxInterval = TimeSpan.FromSeconds(15); // Maximum interval between auto-saving
        private bool _isFirstEventHandled = false; // Flag to track if the first event is handled

        /// <summary>
        /// Default constructor initializes AutoSaver with an empty file path and an empty list of machines.
        /// </summary>
        public AutoSaver() : this(string.Empty, new List<Machines>()) { }

        /// <summary>
        /// Constructor to initialize AutoSaver with a file path and a list of machines.
        /// </summary>
        /// <param name="file">The path of the file.</param>
        /// <param name="machines">The list of machines.</param>
        public AutoSaver(string file, List<Machines> machines)
        {
            if (string.IsNullOrEmpty(file)) // Check if file path is empty or null
                throw new ArgumentNullException(nameof(file), "Переданное имя файла не может быть пустым");
            if (machines == null) // Check if machines list is null
                throw new ArgumentNullException(nameof(machines), "Переданная коллекция машин не может быть null");

            _originalFilePath = file; // Initialize original file path
            _machines = machines; // Initialize list of machines
        }

        /// <summary>
        /// Method to subscribe to updates for a specific machine.
        /// </summary>
        /// <param name="machine">The machine to subscribe to.</param>
        public void SubscribeToMachine(Machines machine)
        {
            if (machine == null) // Check if machine is null
                throw new ArgumentNullException(nameof(machine), "Машина не может быть null");

            if (!_machines.Contains(machine)) // Ensure machine is not already subscribed
            {
                machine.update += HandleUpdate; // Subscribe to machine update event
                _machines.Add(machine); // Add machine to the list
            }
        }

        /// <summary>
        /// Method to subscribe to updates for a specific repair.
        /// </summary>
        /// <param name="repair">The repair to subscribe to.</param>
        public void SubscribeToRepair(Repairs repair)
        {
            if (repair == null) // Check if repair is null
                throw new ArgumentNullException(nameof(repair), "Ремонт не может быть null");

            repair.update += HandleUpdate; // Subscribe to repair update event
        }

        /// <summary>
        /// Event handler method for updates.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        public void HandleUpdate(object? sender, UpdateEventArgs e)
        {
            if (sender == null) return; // If sender is null, exit method

            // Determine if auto-saving should be performed based on time interval and event handling status
            var shouldSave = !_isFirstEventHandled || (DateTime.Now - _lastEventTime) <= _maxInterval;

            if (shouldSave)
            {
                try
                {
                    JsonWriter.SaveToJsonFileTMP(_originalFilePath, _machines); // Save machines data to JSON file
                    Console.WriteLine($"Автосохранение {e.Time} {sender.GetType().Name}"); // Log auto-saving event
                }
                catch (Exception ex)
                {
                    // Log error without interrupting program execution
                    Console.WriteLine($"Ошибка при автосохранении: {ex.Message}");
                }

                _isFirstEventHandled = true; // Mark first event as handled
                _lastEventTime = DateTime.Now; // Update timestamp of last event handled
            }
        }

    }

}
