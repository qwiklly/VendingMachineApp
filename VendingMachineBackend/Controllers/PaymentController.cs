using Microsoft.AspNetCore.Mvc;
using VendingMachineBackend.DTOs;
using VendingMachineBackend.Repositories.Interfaces;

namespace VendingMachineBackend.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController(IPaymentRepository _paymentRepo) : ControllerBase
    {
        /// <summary>
        /// Оплатить заказ (групповая оплата нескольких товаров).
        /// </summary>
        /// <param name="dto">Данные для оплаты.</param>
        [HttpPost]
        [Route("batch")]
        public async Task<ActionResult> PayBatch([FromBody] BatchPaymentDto dto)
        {
            var result = await _paymentRepo.PayBatchAsync(dto);
            return Ok(result);
        }
    }
}
