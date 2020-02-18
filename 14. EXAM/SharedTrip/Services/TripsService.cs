using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<T> GetAll<T>(Func<Trip, T> selectFunc)
        {
            var allProducts = db.Trips.Where(x => x.Seats > 0).Select(selectFunc).ToList();
            return allProducts;
        }

        public void Add(AddTripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Seats = input.Seats,
                Description = input.Description,
                ImagePath = input.ImagePath,
            };

            db.Trips.Add(trip);
            db.SaveChanges();
        }

        public TripDetailsDataModel GetDetails(string id)
        {
            var product = db.Trips.Where(x => x.Id == id)
                .Select(x => new TripDetailsDataModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime,
                    Seats = x.Seats,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                }).FirstOrDefault();
            return product;

        }

        public int FreeSeats(string tripId)
        {
            return db.Trips.FirstOrDefault(x => x.Id == tripId).Seats;
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            var trip = db.Trips.FirstOrDefault(x => x.Id == tripId);
            trip.Seats--;

            var join = new UserTrip
            {
                TripId = tripId,
                UserId = userId,
            };

            db.UserTrips.Add(join);
            
            db.SaveChanges();
        }

        public bool UserJoinTrip(string tripId, string userId)
        {
            return db.UserTrips.Any(x => x.TripId == tripId && x.UserId == userId);
        }
    }
}
