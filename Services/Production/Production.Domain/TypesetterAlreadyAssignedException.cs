using Articles.Exceptions.Domain;

namespace Production.Domain
{
		internal class TypesetterAlreadyAssignedException(string message, Exception? innerException = null) 
				: DomainException(message, innerException)
		{
		}
}
