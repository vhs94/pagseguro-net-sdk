using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Splits;
using PagSeguro.DotNet.Sdk.Orders.Models.Requests;
using PagSeguro.DotNet.Sdk.Orders.Models.Responses;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Splits
{
    /// <inheritdoc cref="ISplitProvider" />
    public class SplitProvider(PagSeguroSettings settings, IFlurlClient flurlClient)
        : BaseProvider(settings, flurlClient),
        ISplitProvider
    {
        /// <inheritdoc />
        public async Task<SplitResponse> GetByIdAsync(string splitId)
        {
            return await Request()
                .AppendPathSegments(OrderEndpoint.Splits, splitId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<SplitResponse>();
        }

        /// <inheritdoc />
        public async Task ReleaseCustodyAsync(
            string splitId,
            SplitCustodyReleaseRequest splitCustodyReleaseRequest)
        {
            await Request()
                .AppendPathSegments(OrderEndpoint.Splits, splitId, OrderEndpoint.CustodyRelease)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(splitCustodyReleaseRequest);
        }
    }
}
