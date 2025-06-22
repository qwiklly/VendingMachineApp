using Microsoft.AspNetCore.Mvc;
using VendingMachineBackend.DTOs;
using VendingMachineBackend.Repositories.Interfaces;

namespace VendingMachineBackend.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController(IPaymentRepository _paymentRepo) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Pay([FromBody] PaymentDto dto)
        {
            var result = await _paymentRepo.PayAsync(dto);
            return Ok(result);
        }
    }
}
