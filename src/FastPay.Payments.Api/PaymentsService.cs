using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FastPay.Payments.Api
{
    public class PaymentsService
    {
        private readonly ILogger<PaymentsService> _logger;

        public PaymentsService(ILogger<PaymentsService> logger)
        {
            _logger = logger;
        }
        
        public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
        {
            if (request.Amount <= 0)
            {
                _logger.LogError($"Invalid amount: {request.Amount}.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                _logger.LogError($"Missing currency.");
                return null;
            }
            
            var paymentId = Guid.NewGuid().ToString("N");
            _logger.LogInformation($"Processing a payment with ID: '{paymentId}' for " +
                                   $"amount: {request.Amount} {request.Currency}...");
            await Task.Delay(3000);
            _logger.LogInformation($"Processed a payment with ID: '{paymentId}'.");

            return new PaymentResponse(paymentId);
        }
    }

    public record PaymentRequest(decimal Amount, string Currency);

    public record PaymentResponse(string PaymentId);
}