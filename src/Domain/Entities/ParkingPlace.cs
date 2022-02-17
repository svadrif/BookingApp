using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ParkingPlace : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Number { get; set; }

        /* EF Relation */
        [JsonIgnore]
        public IEnumerable<Booking> Bookings { get; set; }

        [JsonIgnore]
        public Office Office { get; set; }
        public Guid OfficeId { get; set; }
    }
}