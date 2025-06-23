using static VendingMachineBackend.Responses.CustomResponses;
using VendingMachineBackend.DTOs;

namespace VendingMachineBackend.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<GenericResponse<PaymentResultDto>> PayBatchAsync(BatchPaymentDto dto);
    }
}
