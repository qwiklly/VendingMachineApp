using Microsoft.AspNetCore.Mvc;
using VendingMachineBackend.Repositories.Interfaces;

namespace VendingMachineBackend.Controllers
{
    [ApiController]
    [Route("api/machine")]
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository _repo;

        public MachineController(IMachineRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Заблокировать автомат.
        /// </summary>
        [HttpPost("lock")]
        public async Task<IActionResult> LockMachine()
        {
            var result = await _repo.LockAsync();
            return Ok(result);
        }

        /// <summary>
        /// Разблокировать автомат.
        /// </summary>
        [HttpDelete("lock")]
        public async Task<IActionResult> UnlockMachine()
        {
            var result = await _repo.UnlockAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить текущий статус блокировки автомата.
        /// </summary>
        [HttpGet("lock/status")]
        public async Task<IActionResult> GetLockStatus()
        {
            var result = await _repo.GetLockStatusAsync();
            return Ok(result);
        }
    }
}
