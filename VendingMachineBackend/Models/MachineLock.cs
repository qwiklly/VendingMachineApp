namespace VendingMachineBackend.Models
{
    public class MachineLock
    {
        public int Id { get; set; }
        public bool IsLocked { get; set; }
        public DateTime LockedAt { get; set; }
    }
}
