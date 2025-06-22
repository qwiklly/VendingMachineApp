namespace VendingMachineBackend.DTOs
{
    public class DrinkDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string BrandName { get; set; } = string.Empty;
    }
}
