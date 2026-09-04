using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Resultado da simulação de taxas e parcelamento.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    public class FeeResponse
    {
        /// <summary>
        /// Taxas detalhadas por meio de pagamento.
        /// </summary>
        [JsonPropertyName("payment_methods")]
        public PaymentMethodInfo? PaymentMethods { get; set; }
    }
}
