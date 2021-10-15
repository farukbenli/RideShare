using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace RideShareCase
{
    public class BusinessService : IBusinessService
    {
        public readonly IDataService _dataService;
        private readonly ILogger<BusinessService> _logger;

        public BusinessService(IDataService dataService, ILogger<BusinessService> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public int CreateTripPlan(TripPlanRequest plan)
        {
            return _dataService.CreateTripPlan(plan);
        }

        public void JoinTripPlan(int tripId)
        {
            if (!_dataService.JoinTripPlan(tripId)) throw new BaseException(453);
        }

        public IEnumerable<TripPlan> SearchTripPlans(string fromLocation, string toDestination) // could do better performance with paging
        {
            return _dataService.SearchTripPlans(fromLocation, toDestination);
        }

        public void UpdateTripPlan(int tripId, bool state)
        {
            if (!_dataService.UpdateTripPlan(tripId, state)) throw new BaseException(454);
        }

        public IEnumerable<TripPlan> SearchTripPlansV2(string fromLocation, string toDestination)
        {
            return _dataService.SearchTripPlansV2(fromLocation, toDestination);
        }
    }
}