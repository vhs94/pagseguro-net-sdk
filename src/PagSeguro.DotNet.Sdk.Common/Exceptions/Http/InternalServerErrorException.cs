using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class InternalServerErrorException(string response) : PagSeguroHttpException(
        HttpStatusCode.InternalServerError,
        response,
        ErrorMessages.InternalServerErrorExceptionMessage)
    {
    }
}
