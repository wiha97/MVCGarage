﻿using MVCGarage.Models;
using System.Collections.Generic;

namespace MVCGarage.ViewModels.Garage
{
    public class DisplayVehiclesVM
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public Dictionary<int, ParkingSpot> ParkingSpotsVehicles { get; set; }
        public Dictionary<int, ParkingSpot> BookedParkingSpots { get; set; }
    }
}