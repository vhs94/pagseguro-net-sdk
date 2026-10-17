using PagSeguro.DotNet.Sdk.Orders.Models.Shared;
using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Requests
{
    /// <summary>
    /// Divisão do valor de uma cobrança entre várias contas PagBank. Exige que a
    /// conta principal esteja habilitada como marketplace e que os recebedores
    /// já existam.
    /// <see href="https://developer.pagbank.com.br/docs/config-split">ler documentação</see>
    /// </summary>
    public class SplitRequest
    {
        /// <summary>
        /// Como os valores dos recebedores são interpretados. Use os valores de
        /// <see cref="SplitMethod" />: FIXED ou PERCENTAGE.
        /// </summary>
        public string? Method { get; set; }

        /// <summary>Recebedores da divisão.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<SplitReceiverRequest>? Receivers { get; set; }
    }
}
