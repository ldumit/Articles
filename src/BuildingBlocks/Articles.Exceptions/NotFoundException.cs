using System.Net;

namespace Articles.Exceptions;
public class NotFoundException : HttpException
{
    public NotFoundException(string exceptionMessage = null) : base(HttpStatusCode.NotFound, exceptionMessage)
    {
    }

    public NotFoundException(string exceptionMessage, Exception exception) : base(HttpStatusCode.NotFound, exceptionMessage, exception)
    {
    }
}