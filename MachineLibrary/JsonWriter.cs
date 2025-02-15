using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MachineLibrary
{
    public static class JsonWriter
    {
        /// <summary>
        /// Method to save machines data to a temporary JSON file.
        /// </summary>
        /// <param name="file">The file path to save the data to.</param>
        /// <param name="machines">The list of machines to serialize.</param>
        /// <exception cref="ArgumentException">Thrown when an argument-related error occurs.</exception>
        /// <exception cref="IOException">Thrown when an I/O-related error occurs.</exception>
        /// <exception cref="NotSupportedException">Thrown when an operation is not supported.</exception>
        /// <exception cref="JsonException">Thrown when an error occurs during JSON serialization.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        public static void SaveToJsonFileTMP(string file, List<Machines> machines)
        {
            try
            {
                string filePath = CreateTempFilePath(file); // Create temporary file path

                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Format JSON with indentation
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Use relaxed JSON escaping
                    Converters = { new CustomJsonConverters.CustomDateTimeConverter() }, // Apply custom DateTime converter
                    PropertyNamingPolicy = new CustomJsonConverters.LowerCaseNamingPolicyConverter() // Apply custom naming policy converter
                };

                string jsonString = JsonSerializer.Serialize(machines, options); // Serialize machines data to JSON

                File.WriteAllText(filePath, jsonString, Encoding.UTF8); // Write JSON string to file
            }
            catch (ArgumentException ex) // Catch ArgumentException
            {
                throw new ArgumentException($"ArgumentException: {ex.Message}"); // Throw with additional context
            }
            catch (IOException ex) // Catch IOException
            {
                throw new IOException($"IOException: {ex.Message}"); // Throw with additional context
            }
            catch (NotSupportedException ex) // Catch NotSupportedException
            {
                throw new NotSupportedException($"NotSupportedException: {ex.Message}"); // Throw with additional context
            }
            catch (JsonException ex) // Catch JsonException
            {
                throw new JsonException($"JsonException: {ex.Message}"); // Throw with additional context
            }
            catch (Exception ex) // Catch any other exception
            {
                throw new Exception($"Unexpected Exception: {ex.Message}"); // Throw with additional context
            }
        }

        /// <summary>
        /// Method to save machines data to a JSON file.
        /// </summary>
        /// <param name="file">The file path to save the data to.</param>
        /// <param name="machines">The list of machines to serialize.</param>
        /// <exception cref="ArgumentException">Thrown when an argument-related error occurs.</exception>
        /// <exception cref="IOException">Thrown when an I/O-related error occurs.</exception>
        /// <exception cref="NotSupportedException">Thrown when an operation is not supported.</exception>
        /// <exception cref="JsonException">Thrown when an error occurs during JSON serialization.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        public static void SaveToJsonFile(string file, List<Machines> machines)
        {
            try
            {
                string path = CreateFilePath(file); // Create file path

                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Format JSON with indentation
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Use relaxed JSON escaping
                    Converters = { new CustomJsonConverters.CustomDateTimeConverter() }, // Apply custom DateTime converter
                    PropertyNamingPolicy = new CustomJsonConverters.LowerCaseNamingPolicyConverter() // Apply custom naming policy converter
                };

                string jsonString = JsonSerializer.Serialize(machines, options); // Serialize machines data to JSON

                File.WriteAllText(path, jsonString, Encoding.UTF8); // Write JSON string to file
            }
            catch (ArgumentException ex) // Catch ArgumentException
            {
                throw new ArgumentException($"ArgumentException: {ex.Message}"); // Throw with additional context
            }
            catch (IOException ex) // Catch IOException
            {
                throw new IOException($"IOException: {ex.Message}"); // Throw with additional context
            }
            catch (NotSupportedException ex) // Catch NotSupportedException
            {
                throw new NotSupportedException($"NotSupportedException: {ex.Message}"); // Throw with additional context
            }
            catch (JsonException ex) // Catch JsonException
            {
                throw new JsonException($"JsonException: {ex.Message}"); // Throw with additional context
            }
            catch (Exception ex) // Catch any other exception
            {
                throw new Exception($"Unexpected Exception: {ex.Message}"); // Throw with additional context
            }
        }

        /// <summary>
        /// Method to create a temporary file path based on the original file name.
        /// </summary>
        /// <param name="file">The original file path.</param>
        /// <returns>The temporary file path.</returns>
        private static string CreateTempFilePath(string file)
        {
            string absolutePath = Path.GetFullPath(file); // Get the full path of the original file
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(absolutePath); // Extract file name without extension
            string newFileName = $"{fileNameWithoutExtension}_tmp.json"; // Append '_tmp' to file name
            return newFileName; // Return the new file path
        }

        /// <summary>
        /// Method to create a file path based on the original file name.
        /// </summary>
        /// <param name="file">The original file path.</param>
        /// <returns>The new file path.</returns>
        private static string CreateFilePath(string file)
        {
            string absolutePath = Path.GetFullPath(file); // Get the full path of the original file
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(absolutePath); // Extract file name without extension
            string newFileName = $"{fileNameWithoutExtension}.json"; // Append '.json' to file name
            return newFileName; // Return the new file path
        }


    }
}
