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
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Vehicle ID")]
        public int? VehicleID { get; set; }

        [Display(Name = "Identifiant")]
        public string Label { get; set; }

        [Display(Name="Fee")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Fee { get; set; }

        [Display(Name = "Reserved vehicle type")]
        public ETypeVehicle VehicleType { get; set; }
    }
}