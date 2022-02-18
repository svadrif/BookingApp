using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Map : BaseEntity<Guid>
    {
        public int Floor { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasConfRoom { get; set; }

        /* EF Relation */
        public Office Office { get; set; }
        public Guid OfficeId { get; set; }
        public IEnumerable<WorkPlace> WorkPlaces { get; set; }
    }
}
