using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class ConflictException(string response) : PagSeguroHttpException(
        HttpStatusCode.Conflict,
        response,
        ErrorMessages.ConflicExceptionMessage)
    {
    }
}
