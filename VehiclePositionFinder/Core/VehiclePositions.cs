
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VehiclePositionFinder.Models;

namespace VehiclePositionFinder.Core
{
    internal static class VehiclePositions
    {
        private static List<Position> _positions;
        private static List<Position> Positions
        {
            get
            {
                if(_positions == null)
                {
                    _positions = new List<Position>
                    {
                        new Position { PositionId = 1, Latitude = 34.544909f, Longitude = -102.100843f },
                        new Position { PositionId = 2, Latitude = 32.345544f, Longitude = -99.123124f },
                        new Position { PositionId = 3, Latitude = 33.234235f, Longitude = -100.214124f },
                        new Position { PositionId = 4, Latitude = 35.195739f, Longitude = -95.348899f },
                        new Position { PositionId = 5, Latitude = 31.895839f, Longitude = -97.789573f },
                        new Position { PositionId = 6, Latitude = 32.895839f, Longitude = -101.789573f },
                        new Position { PositionId = 7, Latitude = 34.115839f, Longitude = -100.225732f },
                        new Position { PositionId = 8, Latitude = 32.335839f, Longitude = -99.992232f },
                        new Position { PositionId = 9, Latitude = 33.535339f, Longitude = -94.792232f },
                        new Position { PositionId = 10, Latitude = 32.234235f, Longitude = -100.222222f }
                    };
                }

                return _positions;
            }
        }

        internal static void FindClosest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Vehicle> vehicles = FileReader.ReadFile(); // read file
            stopwatch.Stop();
            Console.WriteLine($"Read file time : {stopwatch.ElapsedMilliseconds} ms. Found {vehicles.Count} vehicles");

            stopwatch.Restart();
            foreach (var position in Positions)
            {
                Vehicle? nearestVehiclePosition = null;
                double nearestDistance = double.MaxValue; // restart nearest distance
                foreach (var vehicle in vehicles)
                {
                    double distance = FindDistanceBetweenPoints(position, vehicle);
                    // narrows down the results
                    if (distance > nearestDistance) continue;

                    nearestVehiclePosition = vehicle;
                    nearestDistance = distance;
                }

                Console.WriteLine($"Position #{position.PositionId} - Nearest Vehicle Reg: {nearestVehiclePosition.VehicleRegistration} Long: {nearestVehiclePosition.Longitude} Lat: {nearestVehiclePosition.Latitude} Distance: {nearestDistance:N2} meters");
            }
            stopwatch.Stop();
            Console.WriteLine($"Nearest position finder execution time : {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine();
        }


        /// <summary>
        /// This function finds distance between a position and a vehicle
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="vehicle">Vehicle</param>
        /// <returns></returns>
        public static double FindDistanceBetweenPoints(Position position, Vehicle vehicle)
        {
            double theta = position.Longitude - vehicle.Longitude;
            double dist = Math.Sin(deg2rad(position.Latitude)) * Math.Sin(deg2rad(vehicle.Latitude)) + Math.Cos(deg2rad(position.Latitude)) * Math.Cos(deg2rad(vehicle.Latitude)) * Math.Cos(deg2rad(theta));
            
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            
            dist = dist * 1609.344;
            return (dist);
        }

        /// <summary>
        /// This function converts decimal degrees to radians
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// This function converts radians to decimal degrees
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
