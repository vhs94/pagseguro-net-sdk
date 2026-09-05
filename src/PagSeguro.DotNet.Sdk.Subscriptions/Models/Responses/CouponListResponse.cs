using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Responses
{
    /// <summary>Página de cupons retornada pela listagem.</summary>
    public class CouponListResponse
    {
        /// <summary>Informações de paginação.</summary>
        [JsonPropertyName("result_set")]
        public ResultSet? ResultSet { get; set; }

        /// <summary>Cupons encontrados.</summary>
        public ICollection<CouponResponse> Coupons { get; set; } = [];
    }
}
