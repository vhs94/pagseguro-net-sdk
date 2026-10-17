using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Cupom de desconto vinculado a uma assinatura. Ao criar a assinatura basta
    /// informar o <see cref="Id" />; o nome e o desconto vêm preenchidos na
    /// resposta.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-cupom">ler documentação</see>
    /// </summary>
    public class CouponReference
    {
        /// <summary>Identificador do cupom. Por exemplo, COUP_123.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        /// <summary>Nome do cupom.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>Desconto concedido pelo cupom.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CouponDiscount? Discount { get; set; }
    }
}
