using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Map : BaseEntity<Guid>
    {
        public int Floor { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasConfRoom { get; set; }

        /* EF Relation */
        public IEnumerable<WorkPlace> WorkPlaces { get; set; }
        public Office Office { get; set; }
        public Guid OfficeId { get; set; }
    }
}
