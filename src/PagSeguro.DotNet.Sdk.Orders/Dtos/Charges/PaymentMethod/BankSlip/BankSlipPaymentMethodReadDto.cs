using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip
{
    public class BankSlipPaymentMethodReadDto : BankSlipPaymentMethodDto
    {
        [JsonPropertyName("boleto")]
        public BankSlipReadDto? BankSlip { get; set; }
    }
}
