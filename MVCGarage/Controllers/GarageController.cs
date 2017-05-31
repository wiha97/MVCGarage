﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCGarage.DataAccess;
using MVCGarage.Models;
using MVCGarage.Repositories;
using MVCGarage.ViewModels.Garage;

namespace MVCGarage.Controllers
{
    public class GarageController : Controller
    {
        private VehicleRepository vehicles = new VehicleRepository();
        private ParkingSpotsRepository parkingSpots = new ParkingSpotsRepository();

        // GET: Garage/BookAParkingSpot
        public ActionResult BookAParkingSpot()
        {
            // Allows the user to select a vehicle in the list of already exiting vehicles
            // or to create a new one
            return View(new BookAParkingSpotVM { Vehicles = vehicles.GetAllVehicles() });
        }

        [HttpPost]
        public ActionResult BookAPartkingSpot()
        {
            // We end up here from the "Create" view of "Vehicles", called by the "BookAParkingSpot/Get"
            // Just need to redirect to the origin view
            return RedirectToAction("BookAParkingSpot");
        }

        [HttpGet]
        public ActionResult SelectAParkingSpot(BookAParkingSpotVM viewModel)
        {
            Vehicle vehicle = vehicles.Vehicle(viewModel.VehicleID);

            // Happends if the user clicks on "Submit" without having selected a vehicle
            // or if the URL is manually entered
            if (vehicle == null)
                return RedirectToAction("Index", "ParkingSpots");

            // Allows the user to select an available parking spot (if any), depending on the type of vehicle
            return View(new BookAParkingSpotVM
            {
                VehicleID = viewModel.VehicleID,
                Vehicles = vehicles.GetAllVehicles(),
                ParkingSpots = parkingSpots.AvailableParkingSpots(vehicle.VehicleType)
            });
        }

        [HttpPost]
        public ActionResult ParkingSpotSelected(BookAParkingSpotVM viewModel)
        {
            // Check in the vehicle ID to the parking spot
            parkingSpots.CheckIn(viewModel.ParkingSpotID, viewModel.VehicleID);

            // Displays the chosen parking spot
            return View(new BookedParkingSpotVM
            {
                Vehicle = vehicles.Vehicle(viewModel.VehicleID),
                ParkingSpot = parkingSpots.ParkingSpot(viewModel.ParkingSpotID)
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                vehicles.Dispose();
                parkingSpots.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
