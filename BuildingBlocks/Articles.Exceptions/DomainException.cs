namespace Articles.Exceptions.Domain
{
		public class DomainException(string message, Exception? innerException = null) 
        : Exception(message, innerException)
		{
    }

    public class NotFoundException(string message) : DomainException(message)
    {
    }
}
