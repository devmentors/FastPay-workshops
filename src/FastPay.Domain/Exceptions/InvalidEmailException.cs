namespace FastPay.Domain.Exceptions
{
    public sealed class InvalidEmailException : FastPayException
    {
        public string Email { get; }

        public InvalidEmailException(string email) 
            : base($"Given email: {email} is invalid")
        {
            Email = email;
        }
    }
}