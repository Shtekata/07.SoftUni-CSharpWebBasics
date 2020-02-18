using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var trips = tripsService.GetAll(x => new TripInfoViewModel
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = x.Seats,
            });

            var viewModel = new AllTripsViewModel
            {
                Trips = trips,
            };
            return View(viewModel, "All");
        }

        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            return View();
        }


        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return Redirect("/Trips/Add");
            }

            if (input.Description.Length > 80)
            {
                return Redirect("/Trips/Add");
            }

            if (!input.ImagePath.StartsWith("http"))
            {
                return Redirect("/Trips/Add");
            }

            tripsService.Add(input);

            return Redirect("/");
        }

        public HttpResponse Details(string tripId)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var trip = tripsService.GetDetails(tripId);

            var viewModel = new TripDetailsViewModel
            {
                Id = trip.Id,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = trip.Seats,
                Description = trip.Description,
                ImagePath = trip.ImagePath,
            };

            return View(viewModel);
        }

        public HttpResponse AddToTrip(string tripId, string userId)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/users/login");
            }

            if (tripsService.UserJoinTrip(tripId, userId) || tripsService.FreeSeats(tripId) == 0)
            {
                return Redirect("/Trips/Details?tripId=" + tripId);
            }

            tripsService.AddUserToTrip(tripId, userId);

            return Redirect("/");
        }
    }
}
