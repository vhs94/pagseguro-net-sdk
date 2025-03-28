﻿using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.PaymentMethod.BankSlip
{
    public class BankSlipPaymentMethodWriteDto : BankSlipPaymentMethodDto
    {
        [JsonPropertyName("boleto")]
        public BankSlipWriteDto? BankSlip { get; set; }
    }
}
