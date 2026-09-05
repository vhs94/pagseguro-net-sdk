using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Requests
{
    /// <summary>
    /// Dados enviados para criar ou alterar um assinante.
    /// <see href="https://developer.pagbank.com.br/reference/criar-assinante">ler documentação</see>
    /// </summary>
    public class CustomerRequest : CustomerBase
    {
        /// <summary>Meios de pagamento do assinante. Ao menos um é obrigatório na criação.</summary>
        [JsonPropertyName("billing_info")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<BillingInfo>? BillingInfo { get; set; }
    }
}
