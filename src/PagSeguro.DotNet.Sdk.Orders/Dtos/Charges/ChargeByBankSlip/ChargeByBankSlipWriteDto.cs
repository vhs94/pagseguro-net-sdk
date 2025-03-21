using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip
{
    public class ChargeByBankSlipWriteDto : ChargeByBankSlipDto, IChargeWriteDto
    {
        public ChargeAmountWriteDto? Amount { get; set; }
        [JsonProperty("payment_method")]
        public BankSlipPaymentMethodWriteDto? PaymentMethod { get; set; }
    }
}
