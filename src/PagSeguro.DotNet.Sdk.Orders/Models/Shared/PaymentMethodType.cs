using System.ComponentModel;

namespace PagSeguro.DotNet.Sdk.Orders.Models.Shared
{
    /// <summary>
    /// Tipos de meio de pagamento com cartão suportados pelo SDK.
    /// <see href="https://developer.pagbank.com.br/reference/objeto-charge">ler documentação</see>
    /// </summary>
    public enum PaymentMethodType
    {
        [Description("CREDIT_CARD")]
        CreditCard,
        [Description("DEBIT_CARD")]
        DebitCard,
        [Description("BOLETO")]
        BankSlip,

        [Description("PIX")]
        Pix
    }
}
