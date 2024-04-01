using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip
{
    public class BankSlipPaymentMethodWriteDto : BankSlipPaymentMethodDto
    {
        [JsonProperty("boleto")]
        public BankSlipWriteDto BankSlip { get; set; }
    }
}
