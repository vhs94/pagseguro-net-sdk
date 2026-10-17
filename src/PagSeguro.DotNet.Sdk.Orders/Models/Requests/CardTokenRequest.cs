using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Dados enviados para validar e armazenar um cartão, recebendo de volta um
    /// token reutilizável nas cobranças seguintes.
    /// <para>
    /// Informe <see cref="Encrypted" /> — o caminho recomendado, em que o cartão
    /// é criptografado no navegador com a chave pública — ou os dados abertos do
    /// cartão, o que exige certificação PCI.
    /// </para>
    /// <see href="https://developer.pagbank.com.br/reference/validar-armanezar-cartao-pagbank">ler documentação</see>
    /// </summary>
    public class CardTokenRequest
    {
        /// <summary>Cartão criptografado com a chave pública da conta.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Encrypted { get; set; }

        /// <summary>Número do cartão. Somente para integrações certificadas PCI.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Number { get; set; }

        /// <summary>Mês de expiração, com 2 dígitos.</summary>
        [JsonPropertyName("exp_month")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExpMonth { get; set; }

        /// <summary>Ano de expiração, com 4 dígitos.</summary>
        [JsonPropertyName("exp_year")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExpYear { get; set; }

        /// <summary>Código de segurança do cartão (CVV).</summary>
        [JsonPropertyName("security_code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SecurityCode { get; set; }

        /// <summary>Portador do cartão.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CardTokenHolder? Holder { get; set; }
    }
}
