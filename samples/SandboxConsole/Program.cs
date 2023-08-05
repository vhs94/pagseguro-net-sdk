using PagSeguro.DotNet.Sdk;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.Amount;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges.BankSlip;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Common;
using PagSeguro.DotNet.Sdk.Settings;

var client = new PagSeguroClient(new ClientSettings
{
    ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c",
    ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c",
    Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A",
    Environment = PagSeguroEnvironment.Sandbox
});


//string write = "{\r\n    \"reference_id\": \"ex-00001\",\r\n    \"customer\": {\r\n        \"name\": \"Jose da Silva\",\r\n        \"email\": \"email@test.com\",\r\n        \"tax_id\": \"12345678909\",\r\n        \"phones\": [\r\n            {\r\n                \"country\": \"55\",\r\n                \"area\": \"11\",\r\n                \"number\": \"999999999\",\r\n                \"type\": \"MOBILE\"\r\n            }\r\n        ]\r\n    },\r\n    \"items\": [\r\n        {\r\n            \"reference_id\": \"referencia do item\",\r\n            \"name\": \"nome do item\",\r\n            \"quantity\": 1,\r\n            \"unit_amount\": 500\r\n        }\r\n    ],\r\n    \"qr_codes\": [\r\n        {\r\n            \"amount\": {\r\n                \"value\": 500\r\n            }\r\n        }\r\n    ],\r\n    \"shipping\": {\r\n        \"address\": {\r\n            \"street\": \"Avenida Brigadeiro Faria Lima\",\r\n            \"number\": \"1384\",\r\n            \"complement\": \"apto 12\",\r\n            \"locality\": \"Pinheiros\",\r\n            \"city\": \"São Paulo\",\r\n            \"region_code\": \"SP\",\r\n            \"country\": \"BRA\",\r\n            \"postal_code\": \"01452002\"\r\n        }\r\n    },\r\n    \"notification_urls\": [\r\n        \"https://meusite.com/notificacoes\"\r\n    ],\r\n    \"charges\": [\r\n        {\r\n            \"reference_id\": \"referencia da cobranca\",\r\n            \"description\": \"descricao da cobranca\",\r\n            \"amount\": {\r\n                \"value\": 500,\r\n                \"currency\": \"BRL\"\r\n            },\r\n            \"payment_method\": {\r\n                \"type\": \"CREDIT_CARD\",\r\n                \"installments\": 1,\r\n                \"capture\": true,\r\n                \"card\": {\r\n                    \"number\": \"4111111111111111\",\r\n                    \"exp_month\": \"12\",\r\n                    \"exp_year\": \"2026\",\r\n                    \"security_code\": \"123\",\r\n                    \"holder\": {\r\n                        \"name\": \"Jose da Silva\"\r\n                    },\r\n                    \"store\": false\r\n                }\r\n            },\r\n            \"notification_urls\": [\r\n                \"https://meusite.com/notificacoes\"\r\n            ]\r\n        }\r\n    ]\r\n}";
//var writeDto = JsonConvert.DeserializeObject<ChargedOrderWriteDto<ChargeByCreditCardWriteDto>>(write);


var boleto = await client
    .ForCharge()
    .WithBankSlip()
    .AddBankSlip(new BankSlipWriteDto
    {
        DueDate = DateTime.Now.AddDays(10),
        Holder = new BankSlipHolderDto
        {
            Name = "jose da silva",
            TaxId = "22222222222",
            Email = "jose@email.com",
            Address = new AddressDto
            {
                Street = "Avenida Brigadeiro Faria Lima",
                Number = "1384",
                Locality = "Pinheiros",
                City = "Sao Paulo",
                Region = "Sao Paulo",
                RegionCode = "SP",
                Country = "Brasil",
                PostalCode = "01452002"
            }
        }
    })
    .WithAmount(new ChargeAmountWriteDto
    {
        Currency = "BRL",
        Value = 1000
    })
    .WithNotificationUrl("https://yourserver.com/nas_ecommerce/277be731-3b7c-4dac-8c4e-4c3f4a1fdc46/")
    .WithReferenceId("ex0123")
    .WithDescription("ex0123")
    .ChargeAsync();

Console.ReadLine();
