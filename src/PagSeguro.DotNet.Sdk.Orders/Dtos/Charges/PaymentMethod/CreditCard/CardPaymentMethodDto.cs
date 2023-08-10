using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.CreditCard
{
    public abstract class CardPaymentMethodDto : PaymentMethodDto
    {
        public int Installments { get; set; }
        public bool Capture { get; set; }

        protected CardPaymentMethodDto(PaymentMethodType type)
            : base(type)
        {
        }
    }
}
