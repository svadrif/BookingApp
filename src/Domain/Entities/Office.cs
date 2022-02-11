using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Office : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        /* EF Relation */
        public IEnumerable<Map> Maps { get; set; }
        public IEnumerable<ParkingPlace> ParkingPlaces { get; set; }
    
    }
}
