namespace VendingMachineBackend.DTOs
{
    public class PaymentResultDto
    {
        public string Message { get; set; } = string.Empty;
        public Dictionary<int, int> Change { get; set; } = new();
    }
}
