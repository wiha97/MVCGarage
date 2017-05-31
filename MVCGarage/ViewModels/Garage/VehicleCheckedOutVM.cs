using MVCGarage.Models;

namespace MVCGarage.ViewModels.Garage
{
    public class VehicleCheckedOutVM
    {
        public Vehicle Vehicle { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
    }
}