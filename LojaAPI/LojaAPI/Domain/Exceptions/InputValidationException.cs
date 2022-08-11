namespace LojaAPI.Domain.Exceptions
{
    public class InputValidationException : Exception
    {
        public InputValidationException(string message) : base(message) { }
    }
}
