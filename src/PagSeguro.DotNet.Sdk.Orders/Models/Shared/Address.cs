using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Endereço utilizado no pedido.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-order">ler documentação</see>
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Rua do endereço. De 1 a 160 caracteres.
        /// </summary>
        public string? Street { get; set; }
        /// <summary>
        /// Número do endereço. De 1 a 20 caracteres.
        /// </summary>
        public string? Number { get; set; }
        /// <summary>
        /// Bairro do endereço. De 1 a 60 caracteres.
        /// </summary>
        public string? Locality { get; set; }
        /// <summary>
        /// Cidade do endereço. De 1 a 90 caracteres.
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// Nome do Estado. De 1 a 50 caracteres.
        /// </summary>
        public string? Region { get; set; }
        /// <summary>
        /// Código do Estado, no padrão ISO 3166-2. 2 caracteres.
        /// </summary>
        [JsonPropertyName("region_code")]
        public string? RegionCode { get; set; }
        /// <summary>
        /// País do endereço, no padrão ISO 3166-1 alpha-3.
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// CEP do endereço. 8 caracteres.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }
    }
}
