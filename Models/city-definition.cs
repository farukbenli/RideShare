using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RideShareCase
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short XCoordinate { get; set; }
        public short YCoordinate { get; set; }
        public virtual IList<TripPlan> tripPlans { get; set; }
    }
}
