namespace FastPay.Domain.Exceptions
{
    public sealed class CannotMakeTransferForSameWalletException : FastPayException
    {
        public CannotMakeTransferForSameWalletException() : base("Cannot make a transfer for the same wallet.")
        {
        }
    }
}