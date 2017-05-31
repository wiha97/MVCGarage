using System;
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
using MVCGarage.ViewModels.Shared;

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
            return RedirectToAction("SelectAVehicle", new SelectAVehicleVM { ActionType = EActionType.BookAParkingSpot });
        }

        [HttpGet]
        public ActionResult SelectAVehicle(SelectAVehicleVM viewModel)
        {
            string followingActionName = string.Empty;
            string followingControllerName = "Garage";

            switch (viewModel.ActionType)
            {
                case EActionType.BookAParkingSpot:
                case EActionType.CheckIn:
                    followingActionName = "SelectAParkingSpot";
                    ViewBag.ActionName = "park";
                    break;
                case EActionType.CheckOut:
                    followingActionName = "VehicleCheckedOut";
                    ViewBag.ActionName = "check out";
                    break;
            }

            return View(new SelectAVehicleVM
            {
                ActionType = viewModel.ActionType,
                Vehicles = vehicles.UnparkedVehicles(),
                VehicleID = viewModel.VehicleID,
                FollowingActionName = followingActionName,
                FollowingControllerName = followingControllerName
            });
        }

        [HttpGet]
        public ActionResult SelectAParkingSpot(SelectAParkingSpotVM viewModel)
        {
            Vehicle vehicle = vehicles.Vehicle(viewModel.VehicleID);

            // Happends if the user clicks on "Submit" without having selected a vehicle
            // or if the URL is manually entered
            if (vehicle == null)
                return RedirectToAction("Index", "ParkingSpots");

            string originActionName = string.Empty;
            string originControllerName = "Garage";

            switch (viewModel.ActionType)
            {
                case EActionType.BookAParkingSpot:
                    originActionName = "ParkingSpotBooked";
                    break;
                case EActionType.CheckIn:
                originActionName = "VehicleCheckedIn";
                    break;
            }

            // Allows the user to select an available parking spot (if any), depending on the type of vehicle
            return View(new SelectAParkingSpotVM
            {
                VehicleID = viewModel.VehicleID,
                SelectedVehicle = vehicle,
                ParkingSpots = parkingSpots.AvailableParkingSpots(vehicle.VehicleType),
                ParkingSpotID = viewModel.ParkingSpotID,
                OriginActionName = originActionName,
                OriginControllerName = originControllerName
            });
        }

        [HttpPost]
        public ActionResult SelectAParkingSpot(EActionType actionType)
        {
            // We end up here from the "Create" view of "ParkingSpots", called by the "SelectAParkingSpot/Get"
            // Just need to redirect to the origin view
            return RedirectToAction("SelectAParkingSpot", new { actionType = actionType });
        }

        [HttpPost]
        public ActionResult ParkingSpotBooked(SelectAParkingSpotVM viewModel)
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

        public ActionResult CheckInVehicle()
        {
            // Allows the user to select a vehicle in the list of already exiting vehicles
            // or to create a new one
            return RedirectToAction("SelectAVehicle", new SelectAVehicleVM { ActionType = EActionType.CheckIn });
        }

        public ActionResult CheckOutVehicle()
        {
            // Allows the user to select a vehicle in the list of already exiting vehicles
            // or to create a new one
            return RedirectToAction("SelectAVehicle", new SelectAVehicleVM { ActionType = EActionType.CheckOut });
        }

        [HttpPost]
        public ActionResult VehicleCheckedOut(SelectAParkingSpotVM viewModel)
        {
            // Check in the vehicle ID to the parking spot
            parkingSpots.CheckIn(viewModel.ParkingSpotID, viewModel.VehicleID);
            vehicles.CheckIn(viewModel.VehicleID, viewModel.ParkingSpotID);

            // Displays the chosen parking spot
            return View(new VehicleCheckedOutVM
            {
                Vehicle = vehicles.Vehicle(viewModel.VehicleID),
                ParkingSpot = parkingSpots.ParkingSpot(viewModel.ParkingSpotID)
            });
        }

        [HttpPost]
        public ActionResult VehicleCheckedIn(SelectAParkingSpotVM viewModel)
        {
            // Check in the vehicle ID to the parking spot
            parkingSpots.CheckIn(viewModel.ParkingSpotID, viewModel.VehicleID);
            vehicles.CheckIn(viewModel.VehicleID, viewModel.ParkingSpotID);

            // Displays the chosen parking spot
            return View(new VehicleCheckedInVM
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
