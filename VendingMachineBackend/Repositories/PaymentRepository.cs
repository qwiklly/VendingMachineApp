using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.Data;
using VendingMachineBackend.DTOs;
using VendingMachineBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VendingMachineBackend.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public PaymentRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        private Dictionary<int, int> CalculateChange(int amount)
        {
            var coins = new[] { 10, 5, 2, 1 };
            var result = new Dictionary<int, int>();

            foreach (var coin in coins)
            {
                int count = amount / coin;
                if (count > 0)
                {
                    result[coin] = count;
                    amount -= coin * count;
                }
            }

            return result;
        }

        public async Task<GenericResponse<PaymentResultDto>> PayBatchAsync(BatchPaymentDto dto)
        {
            try
            {
                var drinkIds = dto.Items.Select(i => i.DrinkId).ToList();
                var drinks = await _appDbContext.Drinks
                              .Where(d => drinkIds.Contains(d.Id))
                              .ToListAsync();

                foreach (var item in dto.Items)
                {
                    var drink = drinks.FirstOrDefault(d => d.Id == item.DrinkId);
                    if (drink == null) return new(false, $"Товар {item.DrinkId} не найден.");
                    if (drink.Quantity < item.Count)
                        return new(false, $"Товара {drink.Name} осталось {drink.Quantity}, вы запросили {item.Count}.");
                }

                int totalPrice = dto.Items.Sum(i =>
                {
                    var d = drinks.First(d => d.Id == i.DrinkId);
                    return (int)(d.Price * i.Count);
                });

                int inserted = dto.Coins1 * 1 + dto.Coins2 * 2 + dto.Coins5 * 5 + dto.Coins10 * 10;
                if (inserted < totalPrice)
                    return new(false, $"Недостаточно средств. Внесено: {inserted}р, нужно: {totalPrice}р");

                foreach (var item in dto.Items)
                {
                    var d = drinks.First(d => d.Id == item.DrinkId);
                    d.Quantity -= item.Count;
                }
                await _appDbContext.SaveChangesAsync();

                int change = inserted - totalPrice;
                var changeDict = CalculateChange(change);

                return new(true, "Оплата прошла успешно.", new PaymentResultDto
                {
                    Message = $"Оплачено {totalPrice}р, сдача: {change}р.",
                    Change = changeDict
                });
            }
            catch
            {
                return new(false, "Ошибка при оплате.");
            }
        }
    }
}
