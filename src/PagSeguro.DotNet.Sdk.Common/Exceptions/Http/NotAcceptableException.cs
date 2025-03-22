using PagSeguro.DotNet.Sdk.Common.Helpers;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class NotAcceptableException(string response) : PagSeguroHttpException(
        HttpStatusCode.NotAcceptable,
        response,
        ErrorMessages.NotAcceptableExceptionMessage)
    {
    }
}
