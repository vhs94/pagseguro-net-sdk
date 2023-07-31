using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions
{
    public abstract class BasePagSeguroHttpException<TResponse> : Exception
    {
        public TResponse Response { get; }
        public HttpStatusCode StatusCode { get; }

        public BasePagSeguroHttpException(
            HttpStatusCode statusCode,
            TResponse response,
            string message = null)
            : base(message)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
