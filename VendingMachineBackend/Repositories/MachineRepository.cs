using Microsoft.EntityFrameworkCore;
using VendingMachineBackend.Data;
using VendingMachineBackend.Repositories.Interfaces;
using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.Models;

namespace VendingMachineBackend.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private const int LockTimeoutSeconds = 60;
        public MachineRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<BaseResponse> LockAsync()
        {
            try
            {
                var existing = await _appDbContext.MachineLocks.FirstOrDefaultAsync();
                if (existing != null && existing.IsLocked && (DateTime.UtcNow - existing.LockedAt).TotalSeconds < 60)
                {
                    return new BaseResponse(false, "Автомат уже занят");
                }

                if (existing == null)
                {
                    existing = new MachineLock();
                    _appDbContext.MachineLocks.Add(existing);
                }

                existing.IsLocked = true;
                existing.LockedAt = DateTime.UtcNow;

                await _appDbContext.SaveChangesAsync();

                return new BaseResponse(true, "Автомат заблокирован.");
            }
            catch
            {
                return new BaseResponse(false, "Ошибка при блокировке автомата.");
            }
        }

        public async Task<BaseResponse> UnlockAsync()
        {
            try
            {
                var existing = await _appDbContext.MachineLocks.FirstOrDefaultAsync();
                if (existing != null)
                {
                    existing.IsLocked = false;
                    await _appDbContext.SaveChangesAsync();
                }

                return new BaseResponse(true, "Автомат разблокирован.");
            }
            catch
            {
                return new BaseResponse(false, "Ошибка при разблокировке автомата.");
            }
        }

        public async Task<GenericResponse<bool>> GetLockStatusAsync()
        {
            try
            {
                var existing = await _appDbContext.MachineLocks.FirstOrDefaultAsync();
                bool isLocked = existing != null && existing.IsLocked &&
                                (DateTime.UtcNow - existing.LockedAt).TotalSeconds < LockTimeoutSeconds;

                return new GenericResponse<bool>(true, "Статус получен.", isLocked);
            }
            catch
            {
                return new GenericResponse<bool>(false, "Ошибка при получении статуса блокировки.");
            }
        }
    }
}
