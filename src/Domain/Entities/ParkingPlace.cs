using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ParkingPlace : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }

        /* EF Relation */
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
