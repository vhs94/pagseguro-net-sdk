using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;
using System.Net;

namespace PagSeguro.DotNet.Sdk.Common.Factories
{
    public class PagSeguroHttpExceptionFactory : IPagSeguroHttpExceptionFactory
    {
        public async Task<PagSeguroHttpException> CreateHttpExceptionAsync(IFlurlResponse response)
        {
            string responseBody = await response.GetStringAsync();
            HttpStatusCode httpStatusCode = (HttpStatusCode)response.StatusCode;
            return httpStatusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestException(responseBody),
                HttpStatusCode.Conflict => new ConflictException(responseBody),
                HttpStatusCode.Forbidden => new ForbiddenException(responseBody),
                HttpStatusCode.InternalServerError => new InternalServerErrorException(responseBody),
                HttpStatusCode.NotAcceptable => new NotAcceptableException(responseBody),
                HttpStatusCode.NotFound => new NotFoundException(responseBody),
                HttpStatusCode.Unauthorized => new UnauthorizedException(responseBody),
                _ => new UnknownHttpException(
                                        httpStatusCode,
                                        responseBody,
                                        ErrorMessages.UnkownHttpExceptionMessage)
            };
        }
    }
}
