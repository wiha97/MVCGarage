using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCGarage.Models
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        public ETypeVehicle VehicleType { get; set; }
        public string Owner { get; set; }
        public double Fee { get; set; }
        public string RegistrationPlate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public int? ParkingSpot { get; set; }
    }
}