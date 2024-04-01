﻿using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Card
{
    public class CardWriteDto : CardDto
    {
        public string Number { get; set; }
        [JsonProperty("security_code")]
        public int SecurityCode { get; set; }
    }
}
