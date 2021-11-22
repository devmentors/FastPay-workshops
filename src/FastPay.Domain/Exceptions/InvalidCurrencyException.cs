namespace FastPay.Domain.Exceptions
{
    public sealed class InvalidCurrencyException : FastPayException
    {
        public string Currency { get; }

        public InvalidCurrencyException(string currency) 
            : base($"Currency: '{currency}' is invalid.")
        {
            Currency = currency;
        }
    }
}