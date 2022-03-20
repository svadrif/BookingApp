using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class State
    {
        public Guid StateId { get; set; }
        public string LastCommand { get; set; } = string.Empty;
        public UserState StateNumber { get; set; } = UserState.NotAuthorized;

        /* EF Relation */
        [ForeignKey("AppUser")]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
