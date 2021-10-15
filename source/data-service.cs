using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RideShareCase
{
    public class DataService : IDataService
    {
        public readonly IConfiguration _configuration;
        private readonly ILogger<DataService> _logger;
        public DataService(IConfiguration configuration, ILogger<DataService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public int CreateTripPlan(TripPlanRequest plan)
        {
            TripPlan tempPlan = new TripPlan
            {
                ToDestination = plan.ToDestination,
                FromLocation = plan.FromLocation,
                EmptySeats = plan.EmptySeats,
                Date = plan.Date,
                Explanation = plan.Explanation,
                State = false
            };
            using (RideShareContext db = new RideShareContext(_configuration))
            {
                List<City> cities = new List<City>();
                City fromCity = db.Cities.Where(x => x.Name == plan.FromLocation).SingleOrDefault();
                City toCity = db.Cities.Where(x => x.Name == plan.ToDestination).SingleOrDefault();
                if (fromCity == null || toCity == null) throw new BaseException(455); // cities table data is corrupt
                cities = db.Cities.Where(x => x.XCoordinate >= fromCity.XCoordinate && x.XCoordinate <= toCity.XCoordinate && x.YCoordinate == fromCity.YCoordinate).ToList();
                cities.AddRange(db.Cities.Where(x => x.YCoordinate >= fromCity.YCoordinate && x.YCoordinate <= toCity.YCoordinate && x.XCoordinate == toCity.YCoordinate).ToList());
                tempPlan.CitiesOnTheWay = cities;

                db.TripPlans.Add(tempPlan);
                db.SaveChanges();
                return tempPlan.Id;
            }
        }

        public bool JoinTripPlan(int tripId)
        {
            using (RideShareContext db = new RideShareContext(_configuration))
            {
                TripPlan trip = db.TripPlans.Find(tripId);
                if (trip.EmptySeats > 0) trip.EmptySeats -= 1;
                else return false;
                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<TripPlan> SearchTripPlans(string fromLocation, string toDestination) // to do paging
        {
            List<TripPlan> tripPlans = new List<TripPlan>();
            using (RideShareContext db = new RideShareContext(_configuration))
            {
                tripPlans = db.TripPlans.Where(x => x.FromLocation == fromLocation && x.ToDestination == toDestination && x.State).ToList();
                return tripPlans;
            }
        }

        public bool UpdateTripPlan(int tripId, bool state)
        {
            using (RideShareContext db = new RideShareContext(_configuration))
            {
                TripPlan trip = db.TripPlans.Where(x => x.Id == tripId && x.State != state).SingleOrDefault();
                if (trip == null) return false;
                trip.State = state;
                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<TripPlan> SearchTripPlansV2(string fromLocation, string toDestination) // to do paging
        {
            List<TripPlan> tripPlans = new List<TripPlan>();

            using (RideShareContext db = new RideShareContext(_configuration))
            {
                var temp = db.TripPlans.Select(x => x.CitiesOnTheWay).ToList();
                tripPlans = db.TripPlans.Where(
                    x => (x.FromLocation == fromLocation && x.ToDestination == toDestination) || (x.CitiesOnTheWay.Any(y => y.Name == toDestination) && x.CitiesOnTheWay.Any(y => y.Name == fromLocation)) && x.State
                    ).ToList();
                return tripPlans;
            }
        }
    }
}