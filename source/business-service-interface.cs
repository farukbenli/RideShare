using System.Collections.Generic;

namespace RideShareCase
{
    public interface IBusinessService
    {
        int CreateTripPlan(TripPlanRequest plan);
        void UpdateTripPlan(int tripId, bool state);
        void JoinTripPlan(int tripId);
        IEnumerable<TripPlan> SearchTripPlans(string fromLocation, string toDestination);
        public IEnumerable<TripPlan> SearchTripPlansV2(string fromLocation, string toDestination);
    }
}
