using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Settings;

namespace PagSeguro.DotNet.Sdk.Maps
{
    /// <summary>
    /// Converte as configurações informadas por quem consome a SDK nas
    /// configurações usadas internamente pelos providers.
    /// </summary>
    internal static class ClientSettingsMapper
    {
        internal static PagSeguroSettings ToPagSeguroSettings(this ClientSettings settings)
        {
            return new PagSeguroSettings
            {
                Environment = settings.Environment,
                Token = settings.Token,
                PrivateKey = settings.PrivateKey,
                ClientId = settings.ClientId,
                ClientSecret = settings.ClientSecret
            };
        }
    }
}
