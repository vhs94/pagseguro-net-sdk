using System.ComponentModel;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Common
{
    public enum PaymentMethodType
    {
        [Description("CREDIT_CARD")]
        CreditCard,
        [Description("DEBIT_CARD")]
        DebitCard,
        [Description("BOLETO")]
        BankSlip
    }
}
