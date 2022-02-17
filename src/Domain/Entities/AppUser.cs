using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public long TelegramId { get; set; }
        public Guid LastCommandId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime EmploymentStart { get; set; }
        public DateTime? EmploymentEnd { get; set; }
        public Guid? PrefferdWorkPlaceId { get; set; }
        public bool isDeleted { get; set; }

        /* EF Relation */
        [JsonIgnore]
        public IEnumerable<Vacation> Vacations { get; set; }

        [JsonIgnore]
        public IEnumerable<Booking> Bookings { get; set; }

    }
}