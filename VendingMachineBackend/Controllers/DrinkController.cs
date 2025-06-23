using Microsoft.AspNetCore.Mvc;
using VendingMachineBackend.DTOs;
using VendingMachineBackend.Repositories.Interfaces;
using static VendingMachineBackend.Responses.CustomResponses;

namespace VendingMachineBackend.Controllers
{
    [Route("api/drinks")]
    [ApiController]
    public class DrinkController(IDrinkRepository _drinktrepo) : ControllerBase
    {
        /// <summary>
        /// Добавить новый напиток в каталог.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Add([FromBody] DrinkCreateDto dto)
        {
            var result = await _drinktrepo.AddDrinkAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Получить список всех напитков.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _drinktrepo.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить напитки с фильтрацией по бренду и диапазону цен.
        /// </summary>
        /// <param name="brandId">ID бренда (опционально).</param>
        /// <param name="minPrice">Минимальная цена (опционально).</param>
        /// <param name="maxPrice">Максимальная цена (опционально).</param>
        [HttpGet("filter")]
        public async Task<ActionResult> GetFiltered([FromQuery] int? brandId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var result = await _drinktrepo.GetFilteredAsync(brandId, minPrice, maxPrice);
            return Ok(result);
        }

        /// <summary>
        /// Получить напиток по его ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _drinktrepo.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
