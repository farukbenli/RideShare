using System;
using System.Text.Json.Serialization;

namespace RideShareCase
{
    public class TripPlanRequest
    {
        public string FromLocation { get; set; }
        public string ToDestination { get; set; }
        public DateTime Date { get; set; }
        public int EmptySeats { get; set; }
        public string Explanation { get; set; }
    }
}
