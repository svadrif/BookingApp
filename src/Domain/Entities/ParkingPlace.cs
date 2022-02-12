using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class ParkingPlace : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }

        /* EF Relation */
        public IEnumerable<Booking> Bookings { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
    }
}