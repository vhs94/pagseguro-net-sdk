using Flurl.Http;

namespace PagSeguro.DotNet.Sdk.Common.Interfaces
{
    public interface IPagSeguroHttpExceptionFactory
    {
        Task<Exception> CreateHttpExceptionAsync(IFlurlResponse response);
    }
}
