﻿using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.ChargeByBankSlip
{
    public class ChargeByBankSlipReadDto : ChargeByBankSlipDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedDate { get; set; }
        public ChargeAmountReadDto Amount { get; set; }
        [JsonProperty("payment_response")]
        public PaymentResponseDto PaymentResponse { get; set; }
        [JsonProperty("payment_method")]
        public BankSlipPaymentMethodReadDto PaymentMethod { get; set; }
        public ICollection<LinkDto> Links { get; set; }

        public ChargeByBankSlipReadDto()
        {
            Links = new List<LinkDto>();
        }
    }
}
