﻿using Domain.Common;
using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Vacation : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public DateTime? VacationStart { get; set; }
        public DateTime? VacationEnd { get; set; }

        /* EF Relation */
        [JsonIgnore]
        public AppUser User { get; set; }
        public Guid UserId { get; set; }
    }
}