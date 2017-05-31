﻿using MVCGarage.Models;

namespace MVCGarage.ViewModels.ParkingSpots
{
    public class CreateParkingSpotsVM
    {
        public ParkingSpot ParkingSpot { get; set; }
        public string OriginActionName { get; set; }
        public string OriginControllerName { get; set; }
        public int SelectedVehicleId { get; set; }
    }
}