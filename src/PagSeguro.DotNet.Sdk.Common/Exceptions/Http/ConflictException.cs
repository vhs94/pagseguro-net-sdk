using System.Net;
using PagSeguro.DotNet.Sdk.Common.Helpers;

namespace PagSeguro.DotNet.Sdk.Common.Exceptions.Http
{
    public class ConflictException : PagSeguroHttpException
    {
        public ConflictException(string response)
            : base(
                  HttpStatusCode.Conflict,
                  response,
                  ErrorMessages.ConflicExceptionMessage)
        {
        }
    }
}
