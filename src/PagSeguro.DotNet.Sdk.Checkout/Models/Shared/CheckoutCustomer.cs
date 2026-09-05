using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Cliente pré-preenchido na página de checkout.
    /// Obrigatório quando customer_modifiable é false.
    /// <see href="https://developer.pagbank.com.br/reference/criar-checkout">ler documentação</see>
    /// </summary>
    public class CheckoutCustomer
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Documento de identificação pessoal (CPF/CNPJ) do cliente.
        /// </summary>
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }

        /// <summary>
        /// Telefone do cliente.
        /// </summary>
        public CheckoutPhone? Phone { get; set; }
    }
}
