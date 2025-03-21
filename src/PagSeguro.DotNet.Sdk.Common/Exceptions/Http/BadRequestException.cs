using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class BadRequestException(string response) : PagSeguroHttpException(
        HttpStatusCode.BadRequest,
        response,
        ErrorMessages.BadRequestExceptionMessage)
    {
    }
}
