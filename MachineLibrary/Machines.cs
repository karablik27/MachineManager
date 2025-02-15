using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Reflection.Metadata.BlobBuilder;

namespace MachineLibrary
{
    public class Machines
    {
        // Event to notify about updates in machine data
        public event EventHandler<UpdateEventArgs>? update;

        // ID of the machine
        [JsonPropertyName("machine_id")]
        int _machine_id;
        // Brand of the machine
        [JsonPropertyName("brand")]
        string _brand;
        // Model of the machine
        [JsonPropertyName("model")]
        string _model;
        // Year of manufacture of the machine
        [JsonPropertyName("year")]
        int _year;
        // Price of the machine
        [JsonPropertyName("price")]
        decimal _price;
        // Flag indicating if the machine is ready for use
        [JsonPropertyName("is_ready")]
        bool _is_ready;
        // Collection of repairs associated with the machine
        [JsonPropertyName("repairs")]
        private IEnumerable<Repairs> _repairs;

        // Properties exposing fields with encapsulation
        public int Machine_ID { get => _machine_id; }

        public string Brand
        {
            get => _brand;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _brand = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Передана пустая строка в поле Brand или строка равная null.");
                }
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _model = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Передана пустая строка в поле Model или строка равная null.");
                }
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                if (value > 0)
                {
                    _year = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Значение поля Year пустое либо равно null.");
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value > 0)
                {
                    _price = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Значение поля Price пустое либо равно null.");
                }
            }
        }

        public bool IS_Ready
        {
            get => _is_ready;
            set
            {
                if (value == true)
                {
                    _is_ready = true;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                if (value == false)
                {
                    _is_ready = false;
                }
            }
        }

        public IEnumerable<Repairs> Repairs { get => _repairs; }


        // Constructor for JSON deserialization
        [JsonConstructor]
        public Machines(int machine_id, string brand, string model, int year, decimal price, bool is_ready, IEnumerable<Repairs> repairs)
        {
            if (machine_id > 0)
            {
                _machine_id = machine_id;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (machine_id)");
            }



            if (brand is not null)
            {
                _brand = brand;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (brand).");
            }


            if (model is not null)
            {
                _model = model;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (model).");
            }


            if (year > 0)
            {
                _year = year;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (year)");
            }


            if (price > 0)
            {
                _price = price;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (price)");
            }

            _is_ready = is_ready;

            _repairs = repairs ?? Enumerable.Empty<Repairs>();

            foreach (var repair in Repairs)
            {
                repair.StatusChanged += (sender, e) => CheckAllRepairs();
            }
        }
        // Default constructor
        public Machines() : this(0, string.Empty, string.Empty, 0, 0, false, Enumerable.Empty<Repairs>()) { }

        /// <summary>
        /// Converts machine data to JSON format.
        /// </summary>
        /// <returns>The machine data in JSON format.</returns>
        public string ToJSON()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(new
            {
                MachineId = _machine_id,
                Brand = _brand,
                Model = _model,
                Year = _year,
                Price = _price.ToString(CultureInfo.InvariantCulture),
                IsReady = _is_ready,
                Repairs = _repairs.Select(r => new
                {
                    RepairId = r.Repair_ID,
                    Issue = r.Issue,
                    RepairCost = r.Repair_Cost.ToString(CultureInfo.InvariantCulture),
                    Technician = r.Technician,
                    IsFixed = r.IS_Fixed,
                    RepairDate = r.Repair_Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                })
            }, options);
        }

        /// <summary>
        /// Checks if all repairs are completed and updates machine readiness.
        /// </summary>
        public void CheckAllRepairs()
        {
            _is_ready = _repairs.All(r => r.IS_Fixed); // Check if all repairs are fixed
            update?.Invoke(this, new UpdateEventArgs(DateTime.Now)); // Invoke update event
        }

        /// <summary>
        /// Adds a repair to the machine's collection of repairs.
        /// </summary>
        /// <param name="repair">The repair to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the repair object is null.</exception>
        public void AddRepair(Repairs repair)
        {
            if (repair != null)
            {
                var newList = new List<Repairs>(_repairs);
                newList.Add(repair); // Add the new repair to the list
                _repairs = newList; // Update the repairs collection
                update?.Invoke(this, new UpdateEventArgs(DateTime.Now)); // Invoke update event
            }
            else
            {
                throw new ArgumentNullException(nameof(repair), "The repair object is null."); // Throw exception if repair object is null
            }
        }

    }
}