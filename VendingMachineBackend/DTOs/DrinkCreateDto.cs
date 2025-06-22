﻿namespace VendingMachineBackend.DTOs
{
    public class DrinkCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int BrandId { get; set; }
    }
}
