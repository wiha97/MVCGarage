using MVCGarage.DataAccess;
using MVCGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCGarage.Repositories
{
    public class ParkingSpotsRepository : IDisposable
    {
        private GarageContext db = new GarageContext();

        public IEnumerable<ParkingSpot> ParkingSpots()
        {
            return db.ParkingSpots;
        }

        public ParkingSpot ParkingSpot(int? id)
        {
            return db.ParkingSpots.Find(id);
        }

        public IEnumerable<ParkingSpot> AvailableSpots(ETypeVehicle vehicleType)
        {
            return ParkingSpots().Where(p => p.VehicleType == vehicleType && p.VehicleID == null);
        }

        public ParkingSpot FirstAvailableSpot(ETypeVehicle vehicleType)
        {
            return AvailableSpots(vehicleType).FirstOrDefault();
        }

        public void Add(ParkingSpot parkingSpot)
        {
            db.ParkingSpots.Add(parkingSpot);
            SaveChanges();
        }

        public void Edit(ParkingSpot parkingSpot)
        {
            db.Entry(parkingSpot).State = EntityState.Modified;
            SaveChanges();
        }

        public void CheckIn(int parkingSpotId, int vehicleId)
        {
            ParkingSpot parkingSpot = ParkingSpot(parkingSpotId);
            parkingSpot.VehicleID = vehicleId;
            Edit(parkingSpot);
        }

        public void CheckOut(int parkingSpotId)
        {
            ParkingSpot parkingSpot = ParkingSpot(parkingSpotId);
            parkingSpot.VehicleID = null;
            Edit(parkingSpot);
        }

        public void Delete(int id)
        {
            db.ParkingSpots.Remove(ParkingSpot(id));
            db.SaveChanges();
        }

        private void SaveChanges()
        {
            db.SaveChanges();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}