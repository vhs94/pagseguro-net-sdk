using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Informações do cliente que está realizando o pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Nome do cliente. De 1 a 120 caracteres.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// E-mail do cliente. De 10 a 255 caracteres.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Documento de identificação pessoal (CPF/CNPJ) do cliente. 11 ou 14 caracteres.
        /// </summary>
        [JsonPropertyName("tax_id")]
        public string? TaxId { get; set; }
        /// <summary>
        /// Contém uma lista de telefones do cliente.
        /// </summary>
        public ICollection<Phone> Phones { get; set; }

        public Customer() => Phones = [];
    }
}
