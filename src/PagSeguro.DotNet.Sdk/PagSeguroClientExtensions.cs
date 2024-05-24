using Microsoft.Extensions.DependencyInjection;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk
{
    public static class PagSeguroClientExtensions
    {
        public static IServiceCollection AddPagSeguro(
            this IServiceCollection services,
            Action<ClientSettings> options)
        {
            var clientSettings = new ClientSettings();
            options.Invoke(clientSettings);
            var client = new PagSeguroClient(clientSettings);
            services.AddSingleton<IPagSeguroClient>(_ => client);
            return services;
        }
    }
}
