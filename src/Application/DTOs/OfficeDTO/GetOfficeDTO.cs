﻿namespace Application.DTOs.OfficeDTO
{
    public class GetOfficeDTO
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
    }
}
