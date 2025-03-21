﻿using Newtonsoft.Json;

namespace PagSeguro.DotNet.Sdk.Account.Dtos
{
    public abstract class AccountDto
    {
        public string? Type { get; set; }
        public string? Email { get; set; }
        [JsonProperty("business_category")]
        public string? BusinessCategory { get; set; }
        public PersonDto? Person { get; set; }
    }
}
