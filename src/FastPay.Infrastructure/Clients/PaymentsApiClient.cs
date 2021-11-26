using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FastPay.Application.Clients;

namespace FastPay.Infrastructure.Clients
{
    internal sealed class PaymentsApiClient : IPaymentsApiClient
    {
        private const string Url = "http://localhost:5010/payments";
        private readonly HttpClient _client;

        public PaymentsApiClient(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<PaymentResponseDto> StartPaymentAsync(decimal amount, string currency)
        {
            var response = await _client.PostAsJsonAsync(Url, new { amount, currency });
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PaymentResponseDto>();
            }

            throw new ArgumentException("Payment error.");
        }
    }
}