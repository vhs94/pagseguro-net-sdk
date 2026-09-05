using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Exceptions.Http;

namespace PagSeguro.DotNet.Sdk.Common.Interfaces
{
    /// <summary>
    /// Fábrica das exceções lançadas quando a API do PagBank responde com erro.
    /// </summary>
    public interface IPagSeguroHttpExceptionFactory
    {
        /// <summary>
        /// Cria a exceção correspondente à resposta de erro recebida.
        /// </summary>
        Task<PagSeguroHttpException> CreateHttpExceptionAsync(IFlurlResponse response);
    }
}
