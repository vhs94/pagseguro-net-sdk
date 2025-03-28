﻿using System.Text.Json.Serialization;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.Item;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.QrCode;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Orders
{
    public class OrderWriteDto : OrderDto
    {
        [JsonPropertyName("qr_codes")]
        public ICollection<QrCodeWriteDto> QrCodes { get; set; }
        public ICollection<ItemWriteDto> Items { get; set; }

        public OrderWriteDto()
        {
            QrCodes = [];
            Items = [];
        }
    }
}
