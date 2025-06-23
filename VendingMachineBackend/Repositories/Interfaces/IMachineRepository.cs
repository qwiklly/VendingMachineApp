using static VendingMachineBackend.Responses.CustomResponses;

namespace VendingMachineBackend.Repositories.Interfaces
{
    public interface IMachineRepository
    {
        Task<BaseResponse> LockAsync();
        Task<BaseResponse> UnlockAsync();
        Task<GenericResponse<bool>> GetLockStatusAsync();
    }
}
