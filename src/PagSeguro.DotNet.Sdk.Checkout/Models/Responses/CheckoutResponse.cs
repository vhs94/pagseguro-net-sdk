using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Checkout.Models.Shared;

namespace PagSeguro.DotNet.Sdk.Checkout.Models.Responses
{
    /// <summary>
    /// Checkout retornado pela API.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public class CheckoutResponse : CheckoutBase
    {
        /// <summary>
        /// Identificador do checkout PagBank.
        /// Por exemplo, CHEC_1B8CB683-11A9-4FAC-8FB4-55ECC74106AA.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Situação do checkout. Valores possíveis: ACTIVE e INACTIVE.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Data e horário em que o checkout foi criado.
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Links relacionados ao checkout. A relação PAY aponta para a página de pagamento.
        /// </summary>
        public ICollection<CheckoutLink> Links { get; set; } = [];
    }
}
