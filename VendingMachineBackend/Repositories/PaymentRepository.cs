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

        public async Task<GenericResponse<PaymentResultDto>> PayAsync(PaymentDto dto)
        {
            try
            {
                var drink = await _appDbContext.Drinks.FirstOrDefaultAsync(d => d.Id == dto.DrinkId);

                if (drink == null)
                    return new GenericResponse<PaymentResultDto>(false, "Товар не найден.");

                if (drink.Quantity <= 0)
                    return new GenericResponse<PaymentResultDto>(false, "Товара нет в наличии.");

                int totalInserted = dto.Coins1 * 1 + dto.Coins2 * 2 + dto.Coins5 * 5 + dto.Coins10 * 10;

                if (totalInserted < drink.Price)
                    return new GenericResponse<PaymentResultDto>(false, $"Недостаточно средств. Внесено: {totalInserted}р, нужно: {drink.Price}р");

                int change = (int)(totalInserted - drink.Price);

                // Уменьшаем количество
                drink.Quantity -= 1;
                await _appDbContext.SaveChangesAsync();

                var changeResult = CalculateChange(change);

                return new GenericResponse<PaymentResultDto>(true, "Оплата прошла успешно.", new PaymentResultDto
                {
                    Message = $"Товар {drink.Name} куплен. Сдача: {change}р.",
                    Change = changeResult
                });
            }
            catch
            {
                return new GenericResponse<PaymentResultDto>(false, "Ошибка при оплате.");
            }
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
    }

}
