using MVCGarage.Models;
using MVCGarage.Repositories;
using MVCGarage.ViewModels.Garage;
using MVCGarage.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCGarage.Controllers
{
    public class GarageController : Controller
    {
        private VehicleRepository vehicles = new VehicleRepository();
        private ParkingSpotsRepository parkingSpots = new ParkingSpotsRepository();

        public ActionResult DisplayAllVehicles()
        {
            Dictionary<int, ParkingSpot> dicParkingSpotsVehicles = new Dictionary<int, ParkingSpot>();
            Dictionary<int, ParkingSpot> dicBookedParkingSpots = new Dictionary<int, ParkingSpot>();

            foreach (Vehicle vehicle in vehicles.Vehicles())
            {
                if (vehicle.ParkingSpotID == null)
                {
                    dicParkingSpotsVehicles.Add(vehicle.ID, null);
                    dicBookedParkingSpots.Add(vehicle.ID, parkingSpots.BookedParkingSpot(vehicle.ID));
                }
                else
                {
                    dicParkingSpotsVehicles.Add(vehicle.ID, parkingSpots.ParkingSpot(vehicle.ParkingSpotID));
                    dicBookedParkingSpots.Add(vehicle.ID, null);
                }
            }

            return View(new DisplayVehiclesVM
            {
                Vehicles = vehicles.Vehicles(),
                ParkingSpotsVehicles = dicParkingSpotsVehicles,
                BookedParkingSpots = dicBookedParkingSpots
            });
        }

        public ActionResult DisplayParkedVehicles()
        {
            Dictionary<int, ParkingSpot> dicParkingSpots = new Dictionary<int, ParkingSpot>();
            foreach (Vehicle vehicle in vehicles.ParkedVehicles())
            {
                dicParkingSpots.Add(vehicle.ID, parkingSpots.ParkingSpot(vehicle.ParkingSpotID));
            }

            return View(new DisplayVehiclesVM
            {
                Vehicles = vehicles.ParkedVehicles(),
                ParkingSpotsVehicles = dicParkingSpots
            });
        }

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
                return RedirectToAction("Index");

            string followingActionName = string.Empty;
            string followingControllerName = "Garage";

            switch (viewModel.ActionType)
            {
                case EActionType.BookAParkingSpot:
                    followingActionName = "ParkingSpotBooked";
                    break;
                case EActionType.CheckIn:
                    followingActionName = "VehicleCheckedIn";
                    break;
            }

            // Allows the user to select an available parking spot (if any), depending on the type of vehicle
            return View(new SelectAParkingSpotVM
            {
                VehicleID = viewModel.VehicleID,
                SelectedVehicle = vehicle,
                ParkingSpots = parkingSpots.AvailableParkingSpots(vehicle.VehicleType),
                FollowingActionName = followingActionName,
                FollowingControllerName = followingControllerName
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

        public ActionResult CheckInAVehicle(int? vehicleId)
        {
            Vehicle vehicle = vehicles.Vehicle(vehicleId);

            if (vehicle == null)
                return RedirectToAction("Index");

            return RedirectToAction("SelectAParkingSpot", new SelectAParkingSpotVM
            {
                ActionType = EActionType.CheckIn,
                VehicleID = vehicle.ID,
                SelectedVehicle = vehicles.Vehicle(vehicleId),
                ParkingSpots = parkingSpots.AvailableParkingSpots(vehicle.VehicleType),
                FollowingActionName = "VehicleCheckedIn",
                FollowingControllerName = "Garage"
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

        public ActionResult CheckOutVehicle()
        {
            // Allows the user to select a vehicle in the list of already exiting vehicles
            // or to create a new one
            return RedirectToAction("SelectAVehicle", new SelectAVehicleVM { ActionType = EActionType.CheckOut });
        }

        public ActionResult CheckOutAVehicle(int? vehicleId)
        {
            if (vehicleId == null)
                return RedirectToAction("Index");

            return RedirectToAction("VehicleCheckedOut", new { vehicleId = vehicleId });
        }

        [HttpGet]
        public ActionResult VehicleCheckedOut(int vehicleId)
        {
            Vehicle vehicle = vehicles.Vehicle(vehicleId);

            if (vehicle == null)
                return RedirectToAction("Index", "Home");

            ParkingSpot parkingSpot = parkingSpots.ParkingSpot(vehicle.ParkingSpotID);

            if (parkingSpot == null)
                return RedirectToAction("Index", "Home");

            // Check out the vehicle ID to the parking spot
            DateTime now = DateTime.Now;
            int nbMinutes = (int)Math.Truncate((now - (DateTime)vehicle.CheckInTime).TotalMinutes);
            double totalAmount = nbMinutes * parkingSpot.Fee;

            parkingSpots.CheckOut(vehicle.ParkingSpotID);
            vehicles.CheckOut(vehicleId);

            // Displays the bill
            return View(new VehicleCheckedOutVM
            {
                Vehicle = vehicle,
                ParkingSpot = parkingSpot,
                NbMinutes = nbMinutes,
                CheckOutTime = now,
                TotalAmount = totalAmount
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
