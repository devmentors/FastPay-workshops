using System.Threading.Tasks;
using FastPay.Application.Clients;

namespace FastPay.Infrastructure.Clients
{
    internal sealed class PaymentsApiClient : IPaymentsApiClient
    {
        public Task<PaymentResponseDto> StartPaymentAsync(decimal amount, string currency)
        {
            throw new System.NotImplementedException();
        }
    }
}