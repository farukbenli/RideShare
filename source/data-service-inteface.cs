using System.Collections.Generic;

namespace RideShareCase
{
    public interface IDataService
    {
        int CreateTripPlan(TripPlanRequest plan);
        bool UpdateTripPlan(int tripId, bool state);
        bool JoinTripPlan(int tripId);
        IEnumerable<TripPlan> SearchTripPlans(string fromLocation, string toDestination);
        IEnumerable<TripPlan> SearchTripPlansV2(string fromLocation, string toDestination);
    }
}
