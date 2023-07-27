using Newtonsoft.Json;
using PagSeguro.DotNet.Sdk;
using PagSeguro.DotNet.Sdk.Common.Helpers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Connect.Dtos.Authorization.AuthorizationCode;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Settings;

var client = new PagSeguroClient(new ClientSettings
{
    ClientId = "8815a5e2-616b-4754-a6d1-40d92b71674c",
    ClientSecret = "45e5a4de-b8eb-4b78-9ce6-fad416b1953c",
    Token = "BCABE5E7AA9D43BCBBB76E3C45C1567A",
    Environment = PagSeguroEnvironment.Sandbox
});

//await client.ConnectAsync(new AuthorizationCodeWriteDto
//{
//    Code = "9dd62e730a844c179e018f58cc953796",
//    RedirectUri = "https://google.com",
//    Scope = ApiScopes.ReadAccounts
//});
//var strOrder = File.ReadAllText("order.json");
//var order = JsonConvert.DeserializeObject<OrderWriteDto>(strOrder);

//var result = await client.Order.CreateOrderAsync(order);
//client.ConfigureClientApplication(null, null);
var xpto = await client.Account.CreateAccountAsync(null);


Console.ReadKey();

