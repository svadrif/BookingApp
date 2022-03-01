﻿using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AppUserDTO
{
    public class UpdateAppUserDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field {0} is required")]
        public long TelegramId { get; set; }
        public Guid LastCommandId { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime EmploymentStart { get; set; }
        public DateTime? EmploymentEnd { get; set; }
        public Guid? PrefferdWorkPlaceId { get; set; }
        public bool isDeleted { get; set; }
    }
}