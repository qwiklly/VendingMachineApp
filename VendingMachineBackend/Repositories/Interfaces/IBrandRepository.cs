using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.DTOs;

namespace VendingMachineBackend.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<GenericResponse<List<BrandDto>>> GetAllAsync();
        Task<BaseResponse> AddAsync(string name);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
