using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip
{
    public class BankSlipPaymentMethodReadDto : BankSlipPaymentMethodDto
    {
        [JsonProperty("boleto")]
        public BankSlipReadDto BankSlip { get; set; }
    }
}
