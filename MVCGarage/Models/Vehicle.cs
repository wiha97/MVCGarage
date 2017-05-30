using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCGarage.Models
{
    public class Vehicle
    {
        
        public int ID { get; set; }
        public ETypeVehicle VehicleType { get; set; }
        public string Owner { get; set; }
        public double Fee { get; set; }
        public int RegPlate { get; set; }
        public DateTime ParkingTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int ParkingSpot { get; set; }
    }
}