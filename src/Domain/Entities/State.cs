using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class State
    {
        public Guid StateId { get; set; }
        public string LastCommand { get; set; } = string.Empty;
        public int StateNumber { get; set; }

        /* EF Relation */
        [ForeignKey("AppUser")]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
