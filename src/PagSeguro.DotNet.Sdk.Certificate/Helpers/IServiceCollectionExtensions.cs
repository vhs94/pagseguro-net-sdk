using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Certificate.Interfaces;
using PagSeguro.DotNet.Sdk.Certificate.Providers;

namespace PagSeguro.DotNet.Sdk.Certificate.Helpers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCertificateClient(this IServiceCollection services)
        {
            services.AddScoped<IDigitalCertificateProvider, DigitalCertificateProvider>();
        }
    }
}
