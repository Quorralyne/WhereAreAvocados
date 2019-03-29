using System.Collections.Generic;

namespace WhereAreAvocados.Data.Models.Data
{
    public class Advocate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwitterHandle { get; set; }
        public List<Event> Events { get; set; }
    }
}
