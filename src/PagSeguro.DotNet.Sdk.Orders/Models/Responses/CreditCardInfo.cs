using PagSeguro.DotNet.Sdk.Orders.Converters;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Responses
{
    /// <summary>
    /// Taxas do cartão de crédito, detalhadas por bandeira.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-taxas-transacao">ler documentação</see>
    /// </summary>
    [JsonConverter(typeof(CreditCardBrandConverter))]
    public class CreditCardInfo
    {
        /// <summary>
        /// Planos de parcelamento da bandeira.
        /// </summary>
        public CreditCardBrand? Brand { get; set; }
    }
}
