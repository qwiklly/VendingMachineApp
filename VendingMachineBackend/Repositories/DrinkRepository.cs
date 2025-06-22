using VendingMachineBackend.DTOs;
using Microsoft.EntityFrameworkCore;
using VendingMachineBackend.Data;
using VendingMachineBackend.Models;
using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.Repositories.Interfaces;

namespace VendingMachineBackend.Repositories
{

    public class DrinkRepository : IDrinkRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public DrinkRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GenericResponse<List<DrinkDto>>> GetAllAsync()
        {
            try
            {
                var drinks = await _appDbContext.Drinks
                    .Include(d => d.Brand)
                    .Select(d => new DrinkDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Price = d.Price,
                        Quantity = d.Quantity,
                        BrandName = d.Brand.Name
                    })
                    .ToListAsync();

                return new GenericResponse<List<DrinkDto>>(true, "OK", drinks);
            }
            catch
            {
                return new GenericResponse<List<DrinkDto>>(false, "Ошибка при получении напитков.");
            }
        }

        public async Task<GenericResponse<List<DrinkDto>>> GetFilteredAsync(int? brandId, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var query = _appDbContext.Drinks.Include(d => d.Brand).AsQueryable();

                if (brandId.HasValue)
                    query = query.Where(d => d.BrandId == brandId.Value);

                if (minPrice.HasValue)
                    query = query.Where(d => d.Price >= minPrice.Value);

                if (maxPrice.HasValue)
                    query = query.Where(d => d.Price <= maxPrice.Value);

                var result = await query
                    .Select(d => new DrinkDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Price = d.Price,
                        Quantity = d.Quantity,
                        BrandName = d.Brand.Name
                    })
                    .ToListAsync();

                return new GenericResponse<List<DrinkDto>>(true, "OK", result);
            }
            catch
            {
                return new GenericResponse<List<DrinkDto>>(false, "Ошибка при фильтрации напитков.");
            }
        }

        public async Task<GenericResponse<DrinkDto>> GetByIdAsync(int id)
        {
            try
            {
                var d = await _appDbContext.Drinks.Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
                if (d == null)
                    return new GenericResponse<DrinkDto>(false, "Товар не найден.");

                var dto = new DrinkDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                    Quantity = d.Quantity,
                    BrandName = d.Brand.Name
                };

                return new GenericResponse<DrinkDto>(true, "OK", dto);
            }
            catch
            {
                return new GenericResponse<DrinkDto>(false, "Ошибка при получении товара.");
            }
        }

        public async Task<BaseResponse> AddDrinkAsync(DrinkCreateDto dto)
        {
            try
            {
                var brandExists = await _appDbContext.Brands.AnyAsync(b => b.Id == dto.BrandId);
                if (!brandExists)
                    return new BaseResponse(false, "Такой бренд не существует.");

                var drink = new Drink
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Quantity = dto.Quantity,
                    BrandId = dto.BrandId
                };

                _appDbContext.Drinks.Add(drink);
                await _appDbContext.SaveChangesAsync();

                return new BaseResponse(true, "Товар добавлен.");
            }
            catch
            {
                return new BaseResponse(false, "Ошибка при добавлении товара.");
            }
        }
    }
}
