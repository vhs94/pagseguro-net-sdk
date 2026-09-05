namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Item que compõe o valor da fatura.</summary>
    public class InvoiceItem
    {
        /// <summary>Valor do item.</summary>
        public Money? Amount { get; set; }

        /// <summary>Tipo do item. Por exemplo, PLAN e SETUP_FEE.</summary>
        public string? Type { get; set; }
    }
}
