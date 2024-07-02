using System.Net;

namespace Articles.Exceptions;
public class BadRequestException : HttpException
{
    public BadRequestException(string exceptionMessage = null) : base(HttpStatusCode.BadRequest, exceptionMessage)
    {
    }

    public BadRequestException(string exceptionMessage, Exception exception) : base(HttpStatusCode.BadRequest, exceptionMessage, exception)
    {
    }
}