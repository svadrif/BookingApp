﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Vacation
{
    public class UpdateVacationDTO
    {
        public Guid Id { get; set; }
        public DateTime? VacationStart { get; set; }
        public DateTime? VacationEnd { get; set; }
        public Guid UserId { get; set; }
    }
}
