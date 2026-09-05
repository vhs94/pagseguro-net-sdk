using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Agendamento da liberação automática dos valores retidos em custódia.
    /// <see href="https://developer.pagbank.com.br/reference/crie-e-pague-um-pedido-com-custodia">ler documentação</see>
    /// </summary>
    public class SplitCustodyRelease
    {
        /// <summary>
        /// Data e horário em que a custódia é liberada automaticamente. No
        /// máximo 365 dias a partir da criação da cobrança.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? Scheduled { get; set; }
    }
}
