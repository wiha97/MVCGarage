using MVCGarage.Models;
using System.Data.Entity;

namespace MVCGarage.DataAccess
{
    public class GarageContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }

        public GarageContext()
            : base("DefaultConnection")
        {
        }
    }
}