using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class UnknownHttpException : PagSeguroHttpException
    {
        public UnknownHttpException(
            HttpStatusCode statusCode,
            string response,
            string message)
            : base(statusCode, response, message)
        {
        }
    }
}
