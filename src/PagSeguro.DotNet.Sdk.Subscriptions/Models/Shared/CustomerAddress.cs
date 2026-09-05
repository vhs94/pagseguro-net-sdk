using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Endereço do assinante.</summary>
    public class CustomerAddress
    {
        /// <summary>Rua do endereço. Máximo de 150 caracteres, sem caracteres especiais.</summary>
        public string? Street { get; set; }

        /// <summary>Número do endereço. Máximo de 8 caracteres.</summary>
        public string? Number { get; set; }

        /// <summary>Complemento do endereço. Máximo de 40 caracteres.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Complement { get; set; }

        /// <summary>Bairro do endereço. Máximo de 60 caracteres.</summary>
        public string? Locality { get; set; }

        /// <summary>Cidade do endereço. Máximo de 60 caracteres.</summary>
        public string? City { get; set; }

        /// <summary>Código do Estado, no padrão ISO 3166-2. 2 caracteres.</summary>
        [JsonPropertyName("region_code")]
        public string? RegionCode { get; set; }

        /// <summary>País do endereço. Atualmente apenas BRA.</summary>
        public string? Country { get; set; }

        /// <summary>CEP do endereço. 8 dígitos.</summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }
    }
}
