using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLibrary
{
    /// <summary>
    /// Represents the arguments for the update event.
    /// </summary>
    public class UpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the time of the update.
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEventArgs"/> class with the specified update time.
        /// </summary>
        /// <param name="updateTime">The time of the update.</param>
        public UpdateEventArgs(DateTime updateTime)
        {
            Time = updateTime;
        }
    }
}
