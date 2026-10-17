namespace PagSeguro.DotNet.Sdk.Subscriptions.Models.Shared
{
    /// <summary>
    /// Referência ao pagamento que originou um estorno.
    /// <see href="https://developer.pagbank.com.br/reference/consultar-estorno">ler documentação</see>
    /// </summary>
    public class PaymentReference
    {
        /// <summary>Identificador do pagamento. Por exemplo, PAYM_123.</summary>
        public string? Id { get; set; }

        /// <summary>Valor pago.</summary>
        public Money? Amount { get; set; }
    }
}
