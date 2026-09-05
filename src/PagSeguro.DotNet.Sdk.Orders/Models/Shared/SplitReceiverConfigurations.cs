using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Configurações aplicadas individualmente a um recebedor da divisão.
    /// <see href="https://developer.pagbank.com.br/reference/crie-e-pague-um-pedido-com-custodia">ler documentação</see>
    /// </summary>
    public class SplitReceiverConfigurations
    {
        /// <summary>Retenção do valor devido ao recebedor. Opcional.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SplitCustody? Custody { get; set; }
    }
}
