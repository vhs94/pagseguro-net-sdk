using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class NotFoundException(string response) : PagSeguroHttpException(
        HttpStatusCode.NotFound,
        response,
        ErrorMessages.NotFoundExceptionMessage)
    {
    }
}
