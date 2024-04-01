using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public abstract class PagSeguroHttpException : Exception
    {
        public string Response { get; }
        public HttpStatusCode StatusCode { get; }

        public PagSeguroHttpException(
            HttpStatusCode statusCode,
            string response,
            string message = null)
            : base(message)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
