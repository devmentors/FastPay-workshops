namespace FastPay.Domain.Exceptions
{
    public sealed class InvalidAmountException : FastPayException
    {
        public decimal Amount { get; }

        public InvalidAmountException(decimal amount) 
            : base($"Amount: '{amount}' is invalid.")
        {
            Amount = amount;
        }
    }
}