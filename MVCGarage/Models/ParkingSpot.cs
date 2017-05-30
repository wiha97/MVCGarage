using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGarage.Models
{
    public class ParkingSpot
    {
        [Key]
        public int ID { get; set; }
        public int? VehicleID { get; set; }
        public string Label { get; set; }
        public ETypeVehicle VehicleType {get; set;}
    }
}