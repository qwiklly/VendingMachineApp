using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.DTOs;

namespace VendingMachineBackend.Repositories.Interfaces
{
    public interface IDrinkRepository
    {
        Task<GenericResponse<List<DrinkDto>>> GetAllAsync();
        Task<GenericResponse<List<DrinkDto>>> GetFilteredAsync(int? brandId, decimal? minPrice, decimal? maxPrice);
        Task<GenericResponse<DrinkDto>> GetByIdAsync(int id);
        Task<BaseResponse> AddDrinkAsync(DrinkCreateDto dto);
    }
}
