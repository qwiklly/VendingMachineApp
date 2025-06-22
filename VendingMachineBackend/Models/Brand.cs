namespace VendingMachineBackend.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Drink> Drinks { get; set; } = new List<Drink>();
    }
}
