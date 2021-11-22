namespace FastPay.Domain.Exceptions
{
    public sealed class UnsupportedCurrencyException : FastPayException
    {
        public string Currency { get; }

        public UnsupportedCurrencyException(string currency) 
            : base($"Currency: '{currency}' is unsupported.")
        {
            Currency = currency;
        }
    }
}