using System.Text.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Por quantos ciclos o cupom é aplicado.</summary>
    public class CouponDuration
    {
        /// <summary>
        /// Tipo da duração. Valores aceitos: ONCE, REPEATING e FOREVER.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>Quantidade de ciclos, quando o tipo é REPEATING.</summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Occurrences { get; set; }
    }
}
