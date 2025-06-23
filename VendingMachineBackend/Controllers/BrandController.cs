using Microsoft.AspNetCore.Mvc;
using VendingMachineBackend.Repositories.Interfaces;

namespace VendingMachineBackend.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController(IBrandRepository _brandRepository) : ControllerBase
    {
        /// <summary>
        /// Получить список всех брендов.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _brandRepository.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Добавить новый бренд.
        /// </summary>
        /// <param name="name">Название бренда.</param>
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] string name)
        {
            var result = await _brandRepository.AddAsync(name);
            return Ok(result);
        }

        /// <summary>
        /// Удалить бренд по ID.
        /// </summary>
        /// <param name="id">ID бренда.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _brandRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}
