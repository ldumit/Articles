using Articles.Exceptions.Domain;

namespace Production.Domain
{
		public class TypesetterAlreadyAssignedException(string message, Exception? innerException = null) 
				: DomainException(message, innerException)
		{
		}
}
