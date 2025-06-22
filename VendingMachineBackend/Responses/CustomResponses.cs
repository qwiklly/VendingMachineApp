namespace VendingMachineBackend.Responses
{
    public class CustomResponses
    {
        public record BaseResponse(bool Flag = false, string Message = null!);
        public record GenericResponse<T>(bool Flag = false, string Message = null!, T? Data = default) : BaseResponse(Flag, Message);
    }
}
