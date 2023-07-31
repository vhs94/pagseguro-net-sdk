using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class PagSeguroHttpException : BasePagSeguroHttpException<string>
    {
        public PagSeguroHttpException(
            HttpStatusCode statusCode,
            string response,
            string message = null)
            : base(statusCode, response, message)
        {
        }
    }
}
