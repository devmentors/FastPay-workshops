namespace FastPay.Domain.Exceptions
{
    public class InvalidPasswordException : FastPayException
    {
        public InvalidPasswordException() : base("Defined password is invalid.")
        {
        }
    }
}