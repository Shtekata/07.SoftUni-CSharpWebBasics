﻿using System.Collections.Generic;

namespace SharedTrip.ViewModels.Trips
{
    public class AllTripsViewModel
    {
        public IEnumerable<TripInfoViewModel> Trips { get; set; }
    }
}
