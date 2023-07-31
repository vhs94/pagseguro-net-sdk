using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Common.Factories;
using PagSeguro.DotNet.Sdk.Common.Interfaces;

namespace PagSeguro.DotNet.Sdk.Common.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPagSeguroCommon(this IServiceCollection services)
        {
            services.AddScoped<IPagSeguroHttpExceptionFactory, PagSeguroHttpExceptionFactory>();
        }
    }
}
