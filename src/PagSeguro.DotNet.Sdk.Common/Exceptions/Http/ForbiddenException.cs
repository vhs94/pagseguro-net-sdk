using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class ForbiddenException(string response) : PagSeguroHttpException(
        HttpStatusCode.Forbidden,
        response,
        ErrorMessages.ForbiddenExceptionMessage)
    {
    }
}
