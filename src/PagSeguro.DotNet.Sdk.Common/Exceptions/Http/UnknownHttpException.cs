using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class UnknownHttpException(
        HttpStatusCode statusCode,
        string response,
        string message) : PagSeguroHttpException(statusCode, response, message)
    {
    }
}
