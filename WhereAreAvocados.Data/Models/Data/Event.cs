using System;
using System.Collections.Generic;

namespace WhereAreAvocados.Data.Models.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<Advocate> Advocates { get; set; }
    }
}
