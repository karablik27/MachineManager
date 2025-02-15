using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MachineLibrary
{
    public static class CustomJsonConverters
    {
        /// <summary>
        /// Custom JSON converter for DateTime objects to handle conversion to and from JSON.
        /// </summary>
        public class CustomDateTimeConverter : JsonConverter<DateTime>
        {
            /// <summary>
            /// Method to read DateTime from JSON.
            /// </summary>
            /// <param name="reader">The reader to parse JSON from.</param>
            /// <param name="typeToConvert">The type being converted.</param>
            /// <param name="options">The serialization options to use.</param>
            /// <returns>The deserialized DateTime object.</returns>
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString()); // Parse DateTime from JSON string
            }

            /// <summary>
            /// Method to write DateTime to JSON.
            /// </summary>
            /// <param name="writer">The writer to output JSON to.</param>
            /// <param name="value">The DateTime value to serialize.</param>
            /// <param name="options">The serialization options to use.</param>
            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd")); // Write DateTime as string in specified format
            }
        }

        /// <summary>
        /// Custom JSON naming policy converter to convert property names to lowercase.
        /// </summary>
        public class LowerCaseNamingPolicyConverter : JsonNamingPolicy
        {
            /// <summary>
            /// Method to convert property names to lowercase.
            /// </summary>
            /// <param name="name">The property name to convert.</param>
            /// <returns>The lowercase converted property name.</returns>
            public override string ConvertName(string name) => name.ToLower(); // Convert name to lowercase
        }

    }
}
