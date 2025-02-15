using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MachineLibrary
{
    public class Repairs
    {
        // Event declaration to notify subscribers about updates in the machine's data
        public event EventHandler<UpdateEventArgs>? update;

        // Event declaration to notify subscribers about changes in the status of the machine or its repairs
        public event EventHandler StatusChanged;

        [JsonPropertyName("repair_id")]
        string _repairId;
        [JsonPropertyName("issue")]
        string _issue;
        [JsonPropertyName("repair_cost")]
        decimal _repairCost;
        [JsonPropertyName("technician")]
        string _technician;
        [JsonPropertyName("is_fixed")]
        bool _isFixed;
        [JsonPropertyName("repair_date")]
        DateTime _repairDate;

        public string Repair_ID { get => _repairId; }
        
        public string Issue
        {
            get => _issue;
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    _issue = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Значение поля Issue пустое либо равно null.");
                }
            }
                
        }
        
        public decimal Repair_Cost
        {
            get => _repairCost;
            set
            {
                if(value > 0)
                {
                    _repairCost = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Значение поля RepairCost пустое либо равно null.");
                }
            }
        }
        
        public string Technician
        {
            get => _technician;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _technician = value;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                }
                else
                {
                    throw new ArgumentNullException("Значение поля Technician пустое либо равно null.");
                }
            }
        }
        
        public bool IS_Fixed
        {
            get => _isFixed;
            set
            {
                if (value == true)
                {
                    _isFixed = true;
                    update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
                    StatusChanged?.Invoke(this, EventArgs.Empty); 
                }
                if (value == false)
                {
                    _isFixed = false;
                }
            }
        }
        
        public DateTime Repair_Date
        {
            get => _repairDate;
            set
            {
                _repairDate = value;
                update?.Invoke(this, new UpdateEventArgs(DateTime.Now));
            }
        }

        [JsonConstructor]
        public Repairs(string repair_id, string issue, decimal repair_cost, string technician, bool is_fixed, DateTime repair_date)
        {
            if(repair_id is not null)
            {
                _repairId = repair_id;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (repair_id)");
            }


            if(issue is not null)
            {
                _issue = issue;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (issue)");
            }


            if(repair_cost > 0)
            {
                _repairCost = repair_cost;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (repair_cost)");
            }


            if(technician is not null)
            {
                _technician = technician;
            }
            else
            {
                throw new ArgumentException("Ошибка в конструкторе. (technician)");
            }

            _isFixed = is_fixed;
            _repairDate = repair_date;
        }

        public Repairs() : this(string.Empty, string.Empty, 0, string.Empty, false, DateTime.MinValue) { }

        
    }

    
}
