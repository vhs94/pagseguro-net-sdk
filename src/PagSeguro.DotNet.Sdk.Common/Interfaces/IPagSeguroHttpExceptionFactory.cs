using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;

namespace PagSeguro.DotNet.Sdk.Common.Interfaces
{
    public interface IPagSeguroHttpExceptionFactory
    {
        Task<PagSeguroHttpException> CreateHttpExceptionAsync(IFlurlResponse response);
    }
}
