using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Retenção dos valores devidos a um recebedor. Enquanto a custódia não é
    /// liberada o valor fica bloqueado na conta do recebedor.
    /// <see href="https://developer.pagbank.com.br/reference/crie-e-pague-um-pedido-com-custodia">ler documentação</see>
    /// </summary>
    public class SplitCustody
    {
        /// <summary>Indica se o valor do recebedor fica retido.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Apply { get; set; }

        /// <summary>
        /// Agendamento da liberação. Se não for informado, a liberação precisa
        /// ser feita manualmente.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SplitCustodyRelease? Release { get; set; }
    }
}
