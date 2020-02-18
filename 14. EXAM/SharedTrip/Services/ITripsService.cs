using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        IEnumerable<T> GetAll<T>(Func<Trip, T> selectFunc);

        TripDetailsDataModel GetDetails(string id);

        public void Add(AddTripInputModel input);

        public void AddUserToTrip(string tripId, string userId);

        public bool UserJoinTrip(string tripId, string userId);

        public int FreeSeats(string tripId);
    }
}
