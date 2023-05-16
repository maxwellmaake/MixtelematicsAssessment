using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiclePositionFinder.Models
{
    internal class Vehicle
    {
        public int VehicleId { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string VehicleRegistration { get; set; }
        public ulong RecordedTimeUTC { get; set; }

        internal static Vehicle FromBytes(byte[] fileBytes, ref int offset)
        {
            Vehicle vehiclePosition = new Vehicle();
            vehiclePosition.VehicleId = BitConverter.ToInt32(fileBytes, offset);
            offset += 4;

            var sb = new StringBuilder();
            while (fileBytes[offset] != 0)
            {
                sb.Append((char)fileBytes[offset]);
                ++offset;
            }

            vehiclePosition.VehicleRegistration = sb.ToString();
            ++offset;

            vehiclePosition.Latitude = BitConverter.ToSingle(fileBytes, offset);
            offset += 4;

            vehiclePosition.Longitude = BitConverter.ToSingle(fileBytes, offset);
            offset += 4;

            var uint64 = BitConverter.ToUInt64(fileBytes, offset);
            vehiclePosition.RecordedTimeUTC = uint64;
            offset += 8;
            return vehiclePosition;
        }
    }
}
