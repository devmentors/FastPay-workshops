using System.Threading.Tasks;

namespace FastPay.Application.Clients
{
    public interface IPaymentsApiClient
    {
        Task<PaymentResponseDto> StartPaymentAsync(decimal amount, string currency);
    }

    public record PaymentResponseDto(string PaymentId);
}