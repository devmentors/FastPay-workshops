namespace FastPay.Domain.Exceptions
{
    public sealed class InvalidTransferAmountException : FastPayException
    {
        public decimal Amount { get; }

        public InvalidTransferAmountException(decimal amount) : base($"Transfer has invalid amount: '{amount}'.")
        {
            Amount = amount;
        }
    }
}