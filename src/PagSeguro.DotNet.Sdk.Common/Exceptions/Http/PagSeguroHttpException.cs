using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public abstract class PagSeguroHttpException(
        HttpStatusCode statusCode,
        string response,
        string? message = null) : Exception(message)
    {
        public string Response { get; } = response;
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
