namespace VendingMachineBackend.DTOs
{
    public class BatchPaymentDto
    {
        public List<ItemDto> Items { get; set; } = new();
        public int Coins1 { get; set; }
        public int Coins2 { get; set; }
        public int Coins5 { get; set; }
        public int Coins10 { get; set; }
    }
}
