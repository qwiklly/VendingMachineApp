using Microsoft.AspNetCore.Mvc;
using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.DTOs;
using VendingMachineBackend.Repositories.Interfaces;

namespace VendingMachineBackend.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController(IBrandRepository _brandRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GenericResponse<List<BrandDto>>>> GetAll()
        {
            var result = await _brandRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> Add([FromBody] string name)
        {
            var result = await _brandRepository.AddAsync(name);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> Delete(int id)
        {
            var result = await _brandRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}
