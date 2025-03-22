using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class UnauthorizedException(string response) : PagSeguroHttpException(
        HttpStatusCode.Unauthorized,
        response,
        ErrorMessages.UnauthorizedExceptionMessage)
    {
    }
}
