using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Recebedor secundário de uma divisão de pagamento.
    /// <see href="https://developer.pagbank.com.br/docs/config-split">ler documentação</see>
    /// </summary>
    public class SplitReceiverRequest
    {
        /// <summary>Conta que recebe a parcela.</summary>
        public SplitAccount? Account { get; set; }

        /// <summary>Parcela destinada ao recebedor.</summary>
        public SplitAmount? Amount { get; set; }

        /// <summary>Descrição livre da parcela.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        /// <summary>Configurações específicas do recebedor, como a custódia.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SplitReceiverConfigurations? Configurations { get; set; }
    }
}
