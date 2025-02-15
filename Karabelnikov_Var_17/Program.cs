using MachineLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;
// C:\Users\Степан\Downloads\17V.json
namespace Karabelnikov_Var_17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Calling a method that implements all the tasks that should be in the program.
            MainRun.LoadAndDisplayMachinesFromJson();
        }
    }
}