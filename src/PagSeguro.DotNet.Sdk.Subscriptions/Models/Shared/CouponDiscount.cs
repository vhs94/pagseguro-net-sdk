namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Desconto concedido pelo cupom.</summary>
    public class CouponDiscount
    {
        /// <summary>
        /// Tipo do desconto. Valores aceitos pela API: PERCENT e AMOUNT.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Valor do desconto: percentual quando o tipo é PERCENT, ou centavos
        /// quando o tipo é AMOUNT.
        /// </summary>
        public int Value { get; set; }
    }
}
