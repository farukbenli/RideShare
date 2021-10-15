using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RideShareCase
{
    public class TripPlan
    {
        public int Id { get; set; }
        [InverseProperty("Name")]
        public string FromLocation { get; set; }
        [InverseProperty("Name")]
        public string ToDestination { get; set; }
        public DateTime Date { get; set; }
        public int EmptySeats { get; set; }
        public string Explanation { get; set; }
        [JsonIgnore]
        public bool State { get; set; }
        [JsonIgnore]
        public virtual IList<City> CitiesOnTheWay { get; set; }
    }
}
