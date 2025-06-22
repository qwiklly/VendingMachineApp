using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.Data;
using VendingMachineBackend.DTOs;
using VendingMachineBackend.Models;
using VendingMachineBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VendingMachineBackend.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public BrandRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GenericResponse<List<BrandDto>>> GetAllAsync()
        {
            try
            {
                var brands = await _appDbContext.Brands
                    .Select(b => new BrandDto { Id = b.Id, Name = b.Name })
                    .ToListAsync();

                return new GenericResponse<List<BrandDto>>(true, "OK", brands);
            }
            catch
            {
                return new GenericResponse<List<BrandDto>>(false, "Ошибка при получении брендов.");
            }
        }

        public async Task<BaseResponse> AddAsync(string name)
        {
            try
            {
                if (await _appDbContext.Brands.AnyAsync(b => b.Name == name))
                    return new BaseResponse(false, "Бренд с таким именем уже существует.");

                _appDbContext.Brands.Add(new Brand { Name = name });
                await _appDbContext.SaveChangesAsync();
                return new BaseResponse(true, "Бренд добавлен.");
            }
            catch
            {
                return new BaseResponse(false, "Ошибка при добавлении бренда.");
            }
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            try
            {
                var brand = await _appDbContext.Brands.FindAsync(id);
                if (brand == null)
                    return new BaseResponse(false, "Бренд не найден.");

                _appDbContext.Brands.Remove(brand);
                await _appDbContext.SaveChangesAsync();
                return new BaseResponse(true, "Бренд удалён.");
            }
            catch
            {
                return new BaseResponse(false, "Ошибка при удалении бренда.");
            }
        }
    }

}
