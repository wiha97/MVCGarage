using MVCGarage.DataAccess;
using MVCGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

        public void AddParkingSpot(ParkingSpot parkingSpot)
        {
            db.ParkingSpots.Add(parkingSpot);
            SaveChanges();
        }

        public void EditParkingSpot(ParkingSpot parkingSpot)
        {
            db.Entry(parkingSpot).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteParkingSpot(int id)
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