﻿namespace HotelListing.API.Models.HotelDTOS
{
    public class GetHotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Rating { get; set; }
    }
}
