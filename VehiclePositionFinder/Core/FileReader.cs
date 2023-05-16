using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehiclePositionFinder.Models;

namespace VehiclePositionFinder.Core
{
    internal static class FileReader
    {
        internal static List<Vehicle> ReadFile()
        {
            byte[]? fileBytes = null;

            try
            {
                var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(folder, "VehiclePositions.dat");
                if (File.Exists(filePath))
                {
                    fileBytes = File.ReadAllBytes(filePath);
                }
                else
                {
                    Console.WriteLine($"File location not found {filePath}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error reading file. Exception: {ex.ToString()}");
            }

            var positions = new List<Vehicle>();

            if (fileBytes != null)
            {
                int offset = 0;
                while (offset < fileBytes.Length)
                {
                    var position = Vehicle.FromBytes(fileBytes, ref offset);
                    positions.Add(position);
                }

                return positions;
            }

            return positions;
        }
    }
}
