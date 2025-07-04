﻿namespace VendingMachineBackend.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
