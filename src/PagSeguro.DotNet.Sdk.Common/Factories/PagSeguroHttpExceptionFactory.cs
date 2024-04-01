using System.Net;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Exceptions;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Common.Factories
{
    public class PagSeguroHttpExceptionFactory : IPagSeguroHttpExceptionFactory
    {
        public async Task<PagSeguroHttpException> CreateHttpExceptionAsync(IFlurlResponse response)
        {
            string responseBody = await response.GetStringAsync();
            HttpStatusCode httpStatusCode = (HttpStatusCode)response.StatusCode;
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new BadRequestException(responseBody);
                case HttpStatusCode.Conflict:
                    return new ConflictException(responseBody);
                case HttpStatusCode.Forbidden:
                    return new ForbiddenException(responseBody);
                case HttpStatusCode.InternalServerError:
                    return new InternalServerErrorException(responseBody);
                case HttpStatusCode.NotAcceptable:
                    return new NotAcceptableException(responseBody);
                case HttpStatusCode.NotFound:
                    return new NotFoundException(responseBody);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedException(responseBody);
                default:
                    return new UnknownHttpException(
                        httpStatusCode,
                        responseBody,
                        ErrorMessages.UnkownHttpExceptionMessage);
            }
        }
    }
}
