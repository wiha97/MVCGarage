using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.Garage
{
    public class SelectAParkingSpotVM
    {
        public int VehicleID { get; set; }
        public Vehicle SelectedVehicle { get; set; }

        public IEnumerable<ParkingSpot> ParkingSpots { get; set; }
        public int ParkingSpotID { get; set; }

        public string OriginActionName { get; set; }
        public string OriginControllerName { get; set; }

        public bool CheckInVehicle { get; set; }
    }
}