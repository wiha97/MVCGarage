using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.Garage
{
    public class BookAParkingSpotVM
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public int VehicleID { get; set; }

        public IEnumerable<ParkingSpot> ParkingSpots { get; set; }
        public int ParkingSpotID { get; set; }
    }
}