namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>Referência a uma fatura.</summary>
    public class InvoiceReference
    {
        /// <summary>Identificador da fatura. Por exemplo, INVO_123.</summary>
        public string? Id { get; set; }

        /// <summary>Valor da fatura.</summary>
        public Money? Amount { get; set; }
    }
}
