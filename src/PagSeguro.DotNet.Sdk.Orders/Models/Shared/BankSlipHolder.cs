using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Responsável pelo pagamento do boleto.
    /// <see href="https://developer.pagbank.com.br/reference/criar-pagar-pedido-com-boleto">ler documentação</see>
    /// </summary>
    public class BankSlipHolder : Holder
    {
        /// <summary>
        /// E-mail do responsável. De 10 a 255 caracteres.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Número do documento (CPF/CNPJ) do responsável. 11 ou 14 caracteres.
        /// </summary>
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        /// <summary>
        /// Endereço do responsável pelo pagamento.
        /// </summary>
        public Address? Address { get; set; }
    }
}
