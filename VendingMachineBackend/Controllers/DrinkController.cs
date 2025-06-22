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
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Add([FromBody] DrinkCreateDto dto)
        {
            var result = await _drinktrepo.AddDrinkAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponse<List<DrinkDto>>>> GetAll()
        {
            var result = await _drinktrepo.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<GenericResponse<List<DrinkDto>>>> GetFiltered([FromQuery] int? brandId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var result = await _drinktrepo.GetFilteredAsync(brandId, minPrice, maxPrice);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponse<DrinkDto>>> GetById(int id)
        {
            var result = await _drinktrepo.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
