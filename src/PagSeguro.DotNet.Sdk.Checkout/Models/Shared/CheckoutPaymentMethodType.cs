using System.ComponentModel;

namespace PagSeguro.DotNet.Sdk.Checkout.Models.Shared
{
    /// <summary>
    /// Meios de pagamento que podem ser habilitados no checkout.
    /// </summary>
    public enum CheckoutPaymentMethodType
    {
        /// <summary>Cartão de crédito.</summary>
        [Description("CREDIT_CARD")]
        CreditCard,

        /// <summary>Cartão de débito.</summary>
        [Description("DEBIT_CARD")]
        DebitCard,

        /// <summary>Boleto.</summary>
        [Description("BOLETO")]
        BankSlip,

        /// <summary>Pix.</summary>
        [Description("PIX")]
        Pix
    }
}
